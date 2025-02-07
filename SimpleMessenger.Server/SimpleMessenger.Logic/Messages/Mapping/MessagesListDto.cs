namespace SimpleMessenger.Logic.Messages.Mapping;

public class MessagesListDto
{
    public IList<MessageDto> Messages { get; init; } = default!;
}