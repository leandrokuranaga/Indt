using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Services;
using Contract.Infra.MessageBus.Models;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Contract.Infra.MessageBus.Services
{
    public sealed class ProposalApprovedMessageHandler(
        IContractService contractService,
        ILogger<ProposalApprovedMessageHandler> logger
    ) : IHandleMessages<ProposalApprovedMessage>
    {
        public async Task Handle(ProposalApprovedMessage message)
        {
            logger.LogInformation("Received proposal-approved message for ProposalId {ProposalId}", message.ProposalId);

            var request = new ContractRequest
            {
                Id = message.ProposalId,
                InsuranceNameHolder = message.InsuranceNameHolder,
                CPF = message.CPF,
                Money = message.MonthlyBill
            };

            await contractService.ContractProposalAsync(request);
        }
    }
}
