using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;

namespace Backend.Api.Configurations;

public static class SwaggerSetup
{
    public static IServiceCollection AddingSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        return services.AddSwaggerGen(options =>
        {
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Affiliate API",
                Description = "API that provides endpoints for import financial transaction files and list data of transactions",
                Contact = new OpenApiContact() { Name = "Filipi Mosquini", Email = "mosquinilabs@gmail.com" },
                License = new OpenApiLicense()
                {
                    Name = "MIT License", 
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
            });
        });
    }

    public static IApplicationBuilder UsingSwagger(this IApplicationBuilder builder)
    {
        return builder
            .UseSwagger()
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Affiliate API"));
    }
}