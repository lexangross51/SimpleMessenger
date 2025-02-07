using Microsoft.Extensions.DependencyInjection;
using SimpleMessenger.DataAccess.Storage;
using SimpleMessenger.DataAccess.Storage.Abstractions;

namespace SimpleMessenger.DataAccess;

public static class DiDataAccessExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString) 
        => services.AddTransient<IMessageRepository>(_ => new MessageRepository(connectionString));
}