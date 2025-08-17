using Contract.Domain.Contract.Enums;

namespace Contract.Infra.Http.Proposal.Dtos;

public class ProposalResponse
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public ProposalStatusEnum ProposalStatus { get; set; }
    public string InsuranceType { get; set; }
}