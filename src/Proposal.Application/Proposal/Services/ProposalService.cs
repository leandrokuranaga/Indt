using Mapster;
using Microsoft.Extensions.Logging;
using Proposal.Application.Common;
using Proposal.Application.Proposal.Models.Request;
using Proposal.Application.Proposal.Models.Response;
using Proposal.Application.Validators;
using Proposal.Application.Validators.ProposalValidators;
using Proposal.Domain;
using Proposal.Domain.Enums;
using Proposal.Domain.SeedWork;

namespace Proposal.Application.Proposal.Services;

public class ProposalService(
    INotification notification,
    IProposalRepository repository,
    ILogger<ProposalService> logger
) : BaseService(notification), IProposalService
{
    public async Task<ProposalResponse> CreateProposalAsync(ProposalRequest request)
    {
        Validate(request, new CreateProposalRequestValidator());

        var proposal = Domain.Proposal.Create(request.InsuranceType);
        
        await repository.InsertOrUpdateAsync(proposal);
        await repository.SaveChangesAsync();

        return proposal.Adapt<ProposalResponse>();
    }

    public async Task<BaseResponse<object>> UpdateProposalAsync(int proposalId, EProposalStatus request)
    {
        var proposal = await repository.GetByIdAsync(proposalId, noTracking: false);
        
        if (proposal is null)
        {
            notification.AddNotification("Proposal", "Proposal not found", NotificationModel.ENotificationType.NotFound);
            return null!;
        }
        
        proposal.SetStatus(request);
        
        await repository.UpdateAsync(proposal);
        await repository.SaveChangesAsync();
        
        return BaseResponse<object>.Ok(null);
    }

    public async Task<List<ProposalResponse>> GetProposalsAsync()
    {
        var proposals = await repository.GetAllAsync();

        return proposals.Adapt<List<ProposalResponse>>();
    }

    public async Task<ProposalResponse> GetProposalAsync(int id)
    {
        var proposal = await repository.GetByIdAsync(id, noTracking: true);
        
        if (proposal is null)
        {
            notification.AddNotification("Proposal", "Proposal not found", NotificationModel.ENotificationType.NotFound);
            return null!;
        }

        return proposal.Adapt<ProposalResponse>();
    }
}