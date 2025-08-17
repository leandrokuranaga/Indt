using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proposal.Domain;
using Proposal.Domain.OutboxAggregate;
using Proposal.Domain.SeedWork;
using Proposal.Infra.Repositories;

namespace Proposal.Infra;

public static class InfraModuleDependency
{
    public static IServiceCollection AddInfraModuleDependency(this IServiceCollection services)
    {
        services.AddScoped<IProposalRepository, ProposalRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INotification, Notification>();

        services.AddScoped<IOutboxRepository, OutboxRepository>();
        
        return services;
    }
}