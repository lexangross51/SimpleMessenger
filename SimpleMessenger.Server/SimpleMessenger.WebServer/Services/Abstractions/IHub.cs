using System.Net.WebSockets;

namespace SimpleMessenger.WebServer.Services.Abstractions;

public interface IHub
{
    void AddWebSocket(WebSocket socket);

    Task BroadcastMessageAsync<TMessage>(TMessage message, CancellationToken token = default);
}