using Backend.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infra.Configurations;

public static class IdentityConfiguration
{
    public static IServiceCollection AddDbContextIdentityInjector(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("MySqlConnection");

        services.AddDbContext<IdentityContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection),
                x => x.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

        return services;
    }
}