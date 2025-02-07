using SimpleMessenger.WebServer.Services.Abstractions;

namespace SimpleMessenger.WebServer.Middlewares;

public class CheckWebSocketMiddleware(RequestDelegate next, IHub hub)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/ws" && context.WebSockets.IsWebSocketRequest)
        {
            await HandleWebSocketRequest(context);
        }
        else
        {
            await next(context);
        }
    }

    private async Task HandleWebSocketRequest(HttpContext context)
    {
        var socket = await context.WebSockets.AcceptWebSocketAsync();
        hub.AddWebSocket(socket);

        var buffer = new byte[4 * 1024];
        var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}