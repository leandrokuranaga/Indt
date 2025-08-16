using Microsoft.Extensions.Logging;
using Proposal.Application.Common;
using Proposal.Application.Proposal.Models.Request;
using Proposal.Application.Proposal.Models.Response;
using Proposal.Domain;
using Proposal.Domain.SeedWork;

namespace Proposal.Application.Proposal.Services;

public class ProposalService(
    INotification notification,
    IProposalRepository repository,
    ILogger<ProposalService> logger
) : BaseService(notification), IProposalService
{
    public Task<ProposalResponse> CreateProposalAsync(ProposalRequest request)
    {
        throw new NotImplementedException();
    }
}