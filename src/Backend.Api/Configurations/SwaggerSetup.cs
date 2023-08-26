using Serilog;

namespace Backend.Api.Configurations;

public static class SwaggerSetup
{
    public static IServiceCollection AddingSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        return services.AddSwaggerGen();
    }

    public static IApplicationBuilder UsingSwagger(this IApplicationBuilder builder)
    {
        return builder
            .UseSwagger()
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Affiliates Api"));
    }
}