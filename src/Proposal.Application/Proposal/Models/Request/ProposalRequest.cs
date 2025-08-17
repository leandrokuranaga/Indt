using Proposal.Domain.Enums;

namespace Proposal.Application.Proposal.Models.Request;

public record ProposalRequest
{
    public EInsuranceType InsuranceType { get; set; }
}