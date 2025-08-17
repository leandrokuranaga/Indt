using Proposal.Domain.Enums;
using Proposal.Domain.ProposalAggregate.ValueObjects;

namespace Proposal.Application.Proposal.Models.Request;

public record ProposalRequest
{
    public EInsuranceType InsuranceType { get; set; }
    public string InsuranceNameHolder { get; set; }
    public string CPF { get; set; }
}