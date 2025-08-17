using Contract.Infra.Http.Proposal.Dtos;

namespace Contract.Infra.Http.Proposal;

public interface IProposalService
{
    Task<ProposalResponse> GetProposalAsync(int proposalId);
}