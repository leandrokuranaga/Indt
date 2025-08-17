using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Services;
using Contract.Infra.MessageBus.Models;

namespace Contract.Infra.MessageBus.Services
{
    public sealed class ProposalApprovedMessageHandler(
        IContractService contractService,
        ILogger<ProposalApprovedMessageHandler> logger
    ) : IHandleMessages<string>
    {
        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private sealed record Envelope<T>(string type, int version, T data);

        public async Task Handle(string messageJson)
        {
            Envelope<ProposalApprovedMessage>? env;
            try
            {
                env = JsonSerializer.Deserialize<Envelope<ProposalApprovedMessage>>(messageJson, JsonOpts);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "JSON inválido recebido: {Json}", messageJson);
                return;
            }

            if (env is null)
            {
                logger.LogWarning("Envelope nulo. Payload: {Json}", messageJson);
                return;
            }

            if (env.type != "object" || env.version != 1)
            {
                logger.LogWarning("Mensagem ignorada (type/version inesperados). Payload: {Json}", messageJson);
                return;
            }

            var d = env.data;

            logger.LogInformation(
                "Received proposal-approved message for ProposalId {ProposalId}",
                d.ProposalId
            );

            var request = new ContractRequest
            {
                Id = d.ProposalId,
                InsuranceNameHolder = d.InsuranceNameHolder,
                CPF = d.CPF,
                Money = d.MonthlyBill
            };

            await contractService.ContractProposalAsync(request);
        }
    }
}
