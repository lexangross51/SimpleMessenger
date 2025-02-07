using SimpleMessenger.DataAccess.Models.Abstractions;

namespace SimpleMessenger.DataAccess.Models;

public sealed class Message : IEntity<long>
{
    public long Id { get; set; }

    public string Text { get; set; } = default!;

    public DateTime Timestamp { get; set; }

    public long SequenceNumber { get; set; }
}