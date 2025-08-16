using Contract.Application.Contract.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Application;

public static class ApplicationModuleDependency
{
    public static void AddApplicationModule(this IServiceCollection services)
    {

        services.AddScoped<IContractService, ContractService>();
    }
}