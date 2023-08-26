using Microsoft.AspNetCore.ResponseCompression;

namespace Backend.Api.Configurations;

public static class ResponseCompressionSetup
{
    public static IServiceCollection AddingResponseCompression(this IServiceCollection services)
    {
        return services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
        });
    }
}