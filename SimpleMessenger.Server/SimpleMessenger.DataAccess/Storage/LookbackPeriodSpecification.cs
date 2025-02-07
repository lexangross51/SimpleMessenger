using SimpleMessenger.DataAccess.Storage.Abstractions;

namespace SimpleMessenger.DataAccess.Storage;

public class LookbackPeriodSpecification(TimeSpan lookback) : ISpecification
{
    public IDictionary<string, object> Parameters { get; private set; } = default!;

    public string ToQueryString()
    {
        var lb = DateTime.Now - lookback;
        Parameters = new Dictionary<string, object>()
        {
            { "time", lb }
        };

        return $"timestamp >= @time";
    }
}