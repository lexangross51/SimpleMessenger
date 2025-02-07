using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleMessenger.DataAccess.Storage;
using SimpleMessenger.Logic.Messages.Commands.CreateMessage;
using SimpleMessenger.Logic.Messages.Queries.GetMessages;
using SimpleMessenger.WebServer.Models;
using SimpleMessenger.WebServer.Services.Abstractions;

namespace SimpleMessenger.WebServer.Conrollers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController(IMediator mediator, IHub hub, ILogger<MessagesController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody]CreateMessageVm message)
    {
        var command = new CreateMessageCommand
        {
            Text = message.Text,
            SequenceNumber = message.SequenceNumber,
            CreatedAt = DateTime.Now,
        };

        try
        {
            var result = await mediator.Send(command);

            // Пользователя не волнует, записалось ли что-то в БД. Он только хочет знать,
            // отправилось ли письмо, поэтому просто логируем такую ошибку
            if (result.IsFailed)
            {
                logger.LogError(string.Join("\n\r", result.Errors.Select(e => e.Message)));
            }
            else
            {
                logger.LogDebug(result.Successes.FirstOrDefault()?.Message ??
                    "Сообщение успешно сохранено в БД");
            }

        }
        catch (Exception ex)
        {
            string errorMessage = $"На стороне сервера произошла ошибка: {ex.Message}, Inner: {ex.InnerException?.Message}";
            logger.LogError(errorMessage);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = errorMessage });
        }

        await hub.BroadcastMessageAsync(command);

        return Ok();
    }

    [HttpPost("history")]
    public async Task<IActionResult> Messages([FromBody]GetMessagesDto messages)
    {
        try
        {
            var result = await mediator.Send(new GetMessagesQuery
            {
                Specification = new LookbackPeriodSpecification(messages.LookbackPeriod)
            });

            if (result.IsFailed)
            {
                string errorMessage = string.Join("\n\r", result.Errors.Select(e => e.Message));
                logger.LogDebug($"При получении списка сообщений возникли ошибки: {errorMessage}");

                return BadRequest(new
                {
                    error = errorMessage
                });
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            string errorMessage = $"На стороне сервера произошла ошибка: {ex.Message}, Inner: {ex.InnerException?.Message}";
            logger.LogError(errorMessage);
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = errorMessage });
        }
    }
}