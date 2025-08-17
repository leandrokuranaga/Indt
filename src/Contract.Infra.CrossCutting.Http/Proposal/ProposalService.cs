using Contract.Infra.Http.Proposal.Dtos;
using Microsoft.Extensions.Logging;

namespace Contract.Infra.Http.Proposal;

public class ProposalService(HttpClient client, ILogger<ProposalService> logger) : BaseHttpService(client), IProposalService
{
    public Task<ProposalResponse> GetProposalAsync(int proposalId)
    {
        return GetAsync<ProposalResponse>($"api/v1/proposal/{proposalId}");
    }
}