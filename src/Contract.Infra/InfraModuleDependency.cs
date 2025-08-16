using Contract.Domain.Contract;
using Contract.Domain.SeedWork;
using Contract.Infra.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Infra;

public static class InfraModuleDependency
{
    public static void AddInfraModuleDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotification, Notification>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IContractRepository, ContractRepository>();
    }
}