using Mapster;
using Microsoft.Extensions.Logging;
using Proposal.Application.Common;
using Proposal.Application.Proposal.Models.Request;
using Proposal.Application.Proposal.Models.Response;
using Proposal.Application.Validators.ProposalValidators;
using Proposal.Domain;
using Proposal.Domain.Enums;
using Proposal.Domain.OutboxAggregate;
using Proposal.Domain.ProposalAggregate.ValueObjects;
using Proposal.Domain.SeedWork;
using System.Text.Json;

namespace Proposal.Application.Proposal.Services;

public class ProposalService(
    INotification notification,
    IProposalRepository proposalRepository,
    IOutboxRepository outboxRepository,
    ILogger<ProposalService> logger,
    IUnitOfWork uow
) : BaseService(notification), IProposalService
{
    public async Task<ProposalResponse> CreateProposalAsync(ProposalRequest request)
    {
        Validate(request, new CreateProposalRequestValidator());

        var proposal = Domain.ProposalAggregate.Proposal.Create(request.InsuranceType, request.InsuranceNameHolder, new CPF(request.CPF), new Money(request.MonthlyBill));
        
        await proposalRepository.InsertOrUpdateAsync(proposal);
        await proposalRepository.SaveChangesAsync();

        return proposal.Adapt<ProposalResponse>();
    }

    public async Task<BaseResponse<object>> UpdateProposalAsync(int proposalId, EProposalStatus request)
    {
        var proposal = await proposalRepository.GetByIdAsync(proposalId, noTracking: false);
        
        if (proposal is null)
        {
            notification.AddNotification("Proposal", "Proposal not found", NotificationModel.ENotificationType.NotFound);
            return null!;
        }
        
        proposal.SetStatus(request);

        await uow.BeginTransactionAsync();
        await proposalRepository.UpdateAsync(proposal);
        await proposalRepository.SaveChangesAsync();

        if (request == EProposalStatus.Approved)
        {
            var evt = new
            {
                type = "object",
                version = 1,
                data = new
                {
                    ProposalId = proposal.Id,
                    InsuranceNameHolder = proposal.InsuranceNameHolder,
                    CPF = proposal.CPF.Value.ToString(),                   
                    MonthlyBill = proposal.MonthlyBill.Value            
                }
            };

            var payload = JsonSerializer.Serialize(evt);

            var outbox = new Outbox
            {
                Id = Guid.NewGuid(),
                Type = "plain-json",
                Content = payload,
                OccuredOn = DateTime.UtcNow
            };

            await outboxRepository.InsertOrUpdateAsync(outbox);
            await outboxRepository.SaveChangesAsync();
        }

        await uow.CommitAsync();

        return BaseResponse<object>.Ok(null);
    }

    private Outbox CreateOutboxMessage(string type, object content) => new()
    {
        Id = Guid.NewGuid(),
        Type = type,
        Content = JsonSerializer.Serialize(content),
        OccuredOn = DateTime.UtcNow,
    };

    public async Task<List<ProposalResponse>> GetProposalsAsync()
    {
        var proposals = await proposalRepository.GetAllAsync();

        return proposals.Adapt<List<ProposalResponse>>();
    }

    public async Task<ProposalResponse> GetProposalAsync(int id)
    {
        var proposal = await proposalRepository.GetByIdAsync(id, noTracking: true);
        
        if (proposal is null)
        {
            notification.AddNotification("Proposal", "Proposal not found", NotificationModel.ENotificationType.NotFound);
            return null!;
        }

        return proposal.Adapt<ProposalResponse>();
    }
}