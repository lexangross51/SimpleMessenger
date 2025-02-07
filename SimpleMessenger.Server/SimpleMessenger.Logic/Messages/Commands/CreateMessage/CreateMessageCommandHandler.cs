using FluentResults;
using FluentValidation;
using MediatR;
using SimpleMessenger.DataAccess.Models;
using SimpleMessenger.DataAccess.Storage.Abstractions;

namespace SimpleMessenger.Logic.Messages.Commands.CreateMessage;

internal class CreateMessageCommandHandler(IMessageRepository repos, IValidator<CreateMessageCommand> validator)
    : IRequestHandler<CreateMessageCommand, Result>
{
    public async Task<Result> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken)
            .ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var message = new Message
        {
            SequenceNumber = request.SequenceNumber,
            Text = request.Text,
            Timestamp = request.CreatedAt
        };

        await repos.Create(message, cancellationToken).ConfigureAwait(false);

        return Result.Ok().WithSuccess("Сообщение успешно создано");
    }
}