using Contract.Application.Contract.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Application;

public static class ApplicationModuleDependency
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IContractService, ContractService>();

        return services;
    }
}