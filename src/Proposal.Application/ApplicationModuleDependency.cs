using Microsoft.Extensions.DependencyInjection;
using Proposal.Application.Proposal.Services;

namespace Proposal.Application;

public static class ApplicationModuleDependency
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IProposalService, ProposalService>();

        return services;
    }
}