namespace SimpleMessenger.WebServer.Models;

public class CreateMessageDto
{
    public string Text { get; set; } = default!;

    public long SequenceNumber { get; set; }
}