using Proposal.Application.Common;
using Proposal.Application.Proposal.Models.Request;
using Proposal.Application.Proposal.Models.Response;
using Proposal.Domain.Enums;

namespace Proposal.Application.Proposal.Services;

public interface IProposalService
{
    Task<ProposalResponse> CreateProposalAsync(ProposalRequest request);
    Task<BaseResponse<object>> UpdateProposalAsync(int proposalId, EProposalStatus request);
    Task<List<ProposalResponse>> GetProposalsAsync();
    Task<ProposalResponse> GetProposalAsync(int id);

}