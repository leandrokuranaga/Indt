using Abp.Domain.Entities;
using Proposal.Domain.Enums;
using Proposal.Domain.Proposal.Proposal.ValueObjects;
using Proposal.Domain.ProposalAggregate.ValueObjects;
using Proposal.Domain.SeedWork.Exceptions;
using IAggregateRoot = Proposal.Domain.SeedWork.IAggregateRoot;

namespace Proposal.Domain.ProposalAggregate;

public class Proposal : Entity, IAggregateRoot
{
    public UtcDate CreationDate { get; set; }
    public EProposalStatus ProposalStatus { get; set; }
    public EInsuranceType InsuranceType { get; set; }
    public string InsuranceNameHolder { get; set; }
    public CPF CPF { get; set; }


    public Proposal()
    {

    }

    public Proposal(
        UtcDate creationDate,
        EProposalStatus proposalStatus,
        EInsuranceType insuranceType,
        string insuranceNameHolder,
        CPF cPF
        )
    {
        CreationDate = creationDate;
        ProposalStatus = proposalStatus;
        InsuranceType = insuranceType;
        InsuranceNameHolder = insuranceNameHolder;
        CPF = cPF;
    }

    public static Proposal Create(
        EInsuranceType insuranceType,
        string insuranceNameHolder,
        CPF cPF
        )
    {
        if (!Enum.IsDefined(typeof(EInsuranceType), insuranceType))
            throw new ArgumentException("Invalid insurance", nameof(insuranceType));

        if (string.IsNullOrWhiteSpace(insuranceNameHolder))
            throw new BusinessRulesException("Insurance name holder is mandatory");

        if (cPF is null || !cPF.IsValid)
            throw new BusinessRulesException("Invalid CPF");

        return new Proposal(
            new UtcDate(DateTime.UtcNow),
            EProposalStatus.InAnalysis,
            insuranceType,
            insuranceNameHolder.Trim(),
            cPF
            );
    }

    public void SetStatus(EProposalStatus newStatus)
    {
        if (!Enum.IsDefined(typeof(EProposalStatus), newStatus))
            throw new ArgumentException("Invalid Status", nameof(newStatus));

        ProposalStatus = newStatus;
    }
}