namespace Contract.Infra.Http.Proposal.Dtos;

public class ProposalResponse
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string ProposalStatus { get; set; }
    public string InsuranceType { get; set; }
}