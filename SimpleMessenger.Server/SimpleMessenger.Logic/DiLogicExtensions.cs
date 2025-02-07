using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SimpleMessenger.Logic;

public static class DiLogicExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        return services
            .AddAutoMapper(currentAssembly)
            .AddMediatR(config => config.RegisterServicesFromAssembly(currentAssembly))
            .AddValidatorsFromAssembly(currentAssembly, includeInternalTypes: true);
    }
}