namespace SimpleMessenger.DataAccess.Models.Abstractions;

public interface IEntity<TId>
{
    TId Id { get; set; }
}