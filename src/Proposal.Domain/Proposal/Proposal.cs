using Abp.Domain.Entities;
using Proposal.Domain.Enums;
using IAggregateRoot = Proposal.Domain.SeedWork.IAggregateRoot;

namespace Proposal.Domain;

public class Proposal : Entity, IAggregateRoot
{
    public DateTime CreationDate { get; set; }
    public EProposalStatus ProposalStatus { get; set; }
    public EInsuranceType InsuranceType { get; set; }

    public Proposal()
    {
        
    }
    
    public Proposal(EInsuranceType insuranceType, EProposalStatus proposalStatus, DateTime creationDate )
    {
        CreationDate = creationDate;
        ProposalStatus = proposalStatus;
        InsuranceType = insuranceType;
    }
    
    public static Proposal Create(EInsuranceType insuranceType)
    {
        if (!Enum.IsDefined(typeof(EInsuranceType), insuranceType))
            throw new ArgumentException("Invalid insurance", nameof(insuranceType));

        return new Proposal(insuranceType, EProposalStatus.InAnalysis, DateTime.UtcNow);
    }

    public void SetStatus(EProposalStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(EProposalStatus), newStatus))
            throw new ArgumentException("Invalid Status", nameof(newStatus));

        ProposalStatus = newStatus;
    }
}