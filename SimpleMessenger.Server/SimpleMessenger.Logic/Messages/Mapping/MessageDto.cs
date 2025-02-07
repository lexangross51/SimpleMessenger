namespace SimpleMessenger.Logic.Messages.Mapping;

public class MessageDto
{
    public string Text { get; set; } = default!;

    public DateTime Timestamp { get; set; }

    public long SequenceNumber { get; set; }
}