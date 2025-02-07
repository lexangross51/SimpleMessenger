using SimpleMessenger.WebServer.Services.Abstractions;
using System.Net.WebSockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SimpleMessenger.WebServer.Services;

internal class Hub(ILogger<Hub> logger) : IHub
{
    private readonly HashSet<WebSocket> _sockets = [];
    private readonly object _locker = new();
    private readonly JsonSerializerOptions _options = new()
    {
        Encoder = JavaScriptEncoder.Default
    };

    public void AddWebSocket(WebSocket socket)
    {
        _sockets.Add(socket);
        logger.LogDebug("Добавлено новое подключение");
    }

    public async Task BroadcastMessageAsync<TMessage>(TMessage message, CancellationToken token = default)
    {
        logger.LogDebug($"Начинаем рассылку подключенным клиентам");
        string json = JsonSerializer.Serialize(message, _options);
        var bytes = Encoding.UTF8.GetBytes(json);

        try
        {
            foreach (var socket in _sockets.Where(s => s.State == WebSocketState.Open))
            {
                await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, token);
            }

            lock (_locker)
            {
                _sockets.RemoveWhere(s => s.State != WebSocketState.Open);
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Во время рассылки произошла ошибка: {ex.Message}");
            throw;
        }
    }
}