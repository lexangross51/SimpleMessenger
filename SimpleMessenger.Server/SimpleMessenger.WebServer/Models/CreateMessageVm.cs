namespace SimpleMessenger.WebServer.Models;

public class CreateMessageVm
{
    public string Text { get; set; } = default!;

    public long SequenceNumber { get; set; }
}