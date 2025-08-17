using Proposal.Application.Proposal.Models.Request;
using Proposal.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Proposal.Api.SwaggerExamples.Proposals;

public class CreateProposalExample : IExamplesProvider<ProposalRequest>
{
    public ProposalRequest GetExamples()
    {
        return new ProposalRequest
        {
            InsuranceType = EInsuranceType.Car,
            InsuranceNameHolder = "John Doe",
            CPF = "63674544032",
            MonthlyBill = 199.99m
        };
    }
}