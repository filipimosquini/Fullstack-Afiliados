using Backend.Core.Services;
using Backend.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Ioc.Injectors;

internal static class ServiceInjector
{
    internal static IServiceCollection AddServicesInjector(this IServiceCollection services)
    {
        return services
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ISignInService, SignInService>()
            .AddScoped<IFinancialTransactionService, FinancialTransactionService>()
            .AddScoped<IFileService, FileService>()
            ;
    }
}