using SimpleMessenger.WebServer.Services.Abstractions;

namespace SimpleMessenger.WebServer.Services;

internal static class DiServicesExtensions
{
    internal static IServiceCollection AddServices(this IServiceCollection services) 
        => services.AddSingleton<IHub, Hub>();
}