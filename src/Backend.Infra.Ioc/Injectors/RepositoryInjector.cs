using Backend.Core.Bases.Interfaces;
using Backend.Infra.Repositries;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Ioc.Injectors;

internal static class RepositoryInjector
{
    public static IServiceCollection AddRepositoriesInjector(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}