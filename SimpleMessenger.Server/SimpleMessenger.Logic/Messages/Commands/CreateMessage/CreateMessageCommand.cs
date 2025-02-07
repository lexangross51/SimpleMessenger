using FluentResults;
using MediatR;

namespace SimpleMessenger.Logic.Messages.Commands.CreateMessage;

public class CreateMessageCommand : IRequest<Result>
{
    public string Text { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

    public long SequenceNumber { get; set; }
}