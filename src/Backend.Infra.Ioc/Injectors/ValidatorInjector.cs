using Microsoft.Extensions.DependencyInjection;

namespace Backend.Ioc.Injectors;

internal static class ValidatorInjector
{
    internal static IServiceCollection AddRequestValidatorsInjector(this IServiceCollection services)
    {
        return services;
    }

    internal static IServiceCollection AddModelValidatorsInjector(this IServiceCollection services)
    {
        return services;
    }
}