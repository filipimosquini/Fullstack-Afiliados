using Backend.Core.Services.Contracts.Business;
using Backend.Core.Services.Contracts.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Ioc.Injectors;

internal static class ContractInjector
{
    internal static IServiceCollection AddViewModelContractInjector(this IServiceCollection services)
    {
        return 
            services
                .AddScoped<UserViewModelContract, UserViewModelContract>()
                .AddScoped<SignInViewModelContract, SignInViewModelContract>()
                .AddScoped<FinancialTransactionImportFileViewModelContract, FinancialTransactionImportFileViewModelContract>()
            ;
    }

    internal static IServiceCollection AddBusinessContractInjector(this IServiceCollection services)
    {
        return 
            services
            .AddScoped<FinancialTransactionFileStructureBusinessContract, FinancialTransactionFileStructureBusinessContract>()
            ;
    }
}