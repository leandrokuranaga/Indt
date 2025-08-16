using Contract.Application.Common;
using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Models.Response;
using Contract.Domain.Contract;
using Contract.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace Contract.Application.Contract.Services;

public class ContractService(
    INotification notification,
    IContractRepository repository,
    ILogger<ContractService> logger
    ) : BaseService(notification), IContractService
{
    public Task<ContractResponse> ContractProposalAsync(ContractRequest request)
    {
        throw new NotImplementedException();
    }
}