using Contract.Infra.Http.Proposal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Infra.Http;

public static class AddInfraHttpModule
{
    public static IServiceCollection AddInfraHttp(this IServiceCollection services, IConfiguration configuration)
    {
        var urlProposal = configuration["App:Settings:ProposalUrl"];
        
        services.AddHttpClient<IProposalService, ProposalService>(c =>
        {
            c.BaseAddress = new Uri(urlProposal);
            c.Timeout = TimeSpan.FromSeconds(10);
        });

        return services;
    }
}