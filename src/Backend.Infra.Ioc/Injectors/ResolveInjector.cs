﻿using Microsoft.Extensions.DependencyInjection;

namespace Backend.Ioc.Injectors;

public static class ResolveInjector
{
    public static IServiceCollection AddProjectInjectors(this IServiceCollection services)
    {
        return services
            .AddRepositoriesInjector()
            .AddBusinessContractInjector()
            .AddViewModelContractInjector()
            .AddServicesInjector()
            .AddApplicationServicesInjector();
    }
}