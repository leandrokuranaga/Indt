using Proposal.Domain.Enums;

namespace Proposal.Application.Proposal.Models.Response;

public record ProposalResponse
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public EProposalStatus ProposalStatus { get; set; }
    public EInsuranceType InsuranceType { get; set; }
}