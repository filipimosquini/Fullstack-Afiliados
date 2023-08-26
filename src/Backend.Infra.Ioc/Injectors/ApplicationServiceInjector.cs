using Microsoft.Extensions.DependencyInjection;

namespace Backend.Ioc.Injectors;

internal static class ApplicationServiceInjector
{
    internal static IServiceCollection AddApplicationServicesInjector(this IServiceCollection services)
    {
        return services;
    }
}