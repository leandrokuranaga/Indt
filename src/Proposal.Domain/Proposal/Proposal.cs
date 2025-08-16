using Abp.Domain.Entities;
using Proposal.Domain.Enums;
using IAggregateRoot = Proposal.Domain.SeedWork.IAggregateRoot;

namespace Proposal.Domain;

public class Proposal : Entity, IAggregateRoot
{
    public string CreationDate { get; set; }
    public EProposalStatus ProposalStatus { get; set; }
    public EInsuranceType InsuranceType { get; set; }

    public Proposal()
    {
        
    }
    
    public Proposal(EInsuranceType insuranceType, EProposalStatus proposalStatus, string creationDate )
    {
        CreationDate = creationDate;
        ProposalStatus = proposalStatus;
        InsuranceType = insuranceType;
    }
    
    public static Proposal Create(EInsuranceType insuranceType, EProposalStatus? initialStatus = null)
    {
        var status = initialStatus ?? EProposalStatus.InAnalysis; 
        var creationDate = DateTime.UtcNow.ToString("O");

        if (!Enum.IsDefined(typeof(EInsuranceType), insuranceType))
            throw new ArgumentException("Invalid Insurance type", nameof(insuranceType));

        if (!Enum.IsDefined(typeof(EProposalStatus), status))
            throw new ArgumentException("Invlaid Initial Status", nameof(initialStatus));

        return new Proposal(insuranceType, status, creationDate);
    }

    public void SetStatus(EProposalStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(EProposalStatus), newStatus))
            throw new ArgumentException("Invalid Status", nameof(newStatus));

        ProposalStatus = newStatus;
    }
}