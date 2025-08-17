using Contract.Application.Common;
using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Models.Response;
using Contract.Domain.Contract;
using Contract.Domain.Contract.ValueObjects;
using Contract.Domain.SeedWork;
using Contract.Infra.Http.Proposal;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Contract.Application.Contract.Services;

public class ContractService(
    INotification notification,
    IContractRepository repository,
    ILogger<ContractService> logger,
    IProposalService proposalService
    ) : BaseService(notification), IContractService
{
    public async Task<ContractResponse> ContractProposalAsync(ContractRequest request)
    {
        var proposal = await proposalService.GetProposalAsync(request.Id);
        if (proposal == null)
        {
            notification.AddNotification("Proposal", "Proposal not found", NotificationModel.ENotificationType.NotFound);
            return null!;
        }

        Domain.Contract.Contract contract;

        contract = Domain.Contract.Contract.Create(
            proposalId: request.Id,
            proposalStatus: proposal.ProposalStatus,
            insuranceNameHolder: request.InsuranceNameHolder,
            cpf: new CPF(request.CPF),
            monthlyBill: new Money(request.Money)
        );

        await repository.InsertOrUpdateAsync(contract);
        await repository.SaveChangesAsync();

        return contract.Adapt<ContractResponse>();
    }
    
    public async Task<ContractResponse> GetContractByIdAsync(int contractId)
    {
        var contract = await repository.GetByIdAsync(contractId, noTracking: true);
        if (contract == null)
        {
            notification.AddNotification("Contract", "Contract not found", NotificationModel.ENotificationType.NotFound);
            return null!;
        }
        
        return contract.Adapt<ContractResponse>();
    }
}