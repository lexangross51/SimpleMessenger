using FluentValidation;

namespace SimpleMessenger.Logic.Messages.Commands.CreateMessage;

internal class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(c => c.Text)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(c => c.SequenceNumber)
            .NotEmpty();
    }
}