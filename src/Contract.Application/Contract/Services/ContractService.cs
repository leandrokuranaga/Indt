using Contract.Application.Common;
using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Models.Response;
using Contract.Domain.Contract.Enums;
using Contract.Domain.Contract.ValueObjects;
using Contract.Domain.SeedWork;
using ContractAggregate.Domain;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Contract.Application.Contract.Services;

public class ContractService(
    INotification notification,
    IContractRepository repository,
    ILogger<ContractService> logger
    
    ) : BaseService(notification), IContractService
{
    public async Task<ContractResponse> ContractProposalAsync(ContractRequest request)
    {
        ContractAggregate.Domain.Contract contract;

        contract = ContractAggregate.Domain.Contract.Create(
            proposalId: request.Id,
            proposalStatus: ProposalStatusEnum.Approved,
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
        var contract = await repository.GetByIdAsync(contractId, noTracking: false);

        if (contract == null)
        {
            notification.AddNotification("Contract", "Contract not found", NotificationModel.ENotificationType.NotFound);
            return null!;
        }

        var response = (ContractResponse)contract;

        return response;
    }
}