using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Models.Response;

namespace Contract.Application.Contract.Services;

public interface IContractService
{
    Task<ContractResponse> ContractProposalAsync(ContractRequest request);
    Task<ContractResponse> GetContractByIdAsync(int contractId);
}