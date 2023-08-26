using Backend.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infra.Configurations;

public static class EntityFrameworkConfiguration
{
    public static IServiceCollection AddDbContextInjector(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("MySqlConnection");
        services.AddDbContext<AffiliateContext>(options =>
        {
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection),
                x => x.MigrationsAssembly(typeof(AffiliateContext).Assembly.FullName));
        });

        return services;
    }

    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<AffiliateContext>();

        if (context.MigrateDatabase()) return builder;

        context.Database.Migrate();

        return builder;
    }
}