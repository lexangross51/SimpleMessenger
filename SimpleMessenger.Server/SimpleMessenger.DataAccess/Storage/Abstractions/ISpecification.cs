namespace SimpleMessenger.DataAccess.Storage.Abstractions;

public interface ISpecification
{
    IDictionary<string, object> Parameters { get; }

    string ToQueryString();
}