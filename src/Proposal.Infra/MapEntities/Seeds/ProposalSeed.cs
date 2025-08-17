using Proposal.Domain.Enums;
using Proposal.Domain.Proposal.Proposal.ValueObjects;

namespace Proposal.Infra.MapEntities.Seeds;

public static class ProposalSeed
{
    public static List<Domain.ProposalAggregate.Proposal> Proposals()
    {
        return
        [
            new Domain.ProposalAggregate.Proposal
            {
                Id = 1,
                CreationDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2023, 04, 01), DateTimeKind.Utc)),
                ProposalStatus = EProposalStatus.Approved,
                InsuranceType = EInsuranceType.Life,
                InsuranceNameHolder = "John Doe",
            },
            
            new Domain.ProposalAggregate.Proposal
            {
                Id = 2,
                CreationDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2023, 04, 01), DateTimeKind.Utc)),
                ProposalStatus = EProposalStatus.Approved,
                InsuranceType = EInsuranceType.Health,
                InsuranceNameHolder = "Jane Doe",
            }
        ];
    }

    public static IEnumerable<object> CpfOwned() =>
    [
        new { ProposalId = 1, Value = "07038612042" },
        new { ProposalId = 2, Value = "20791888010" }
    ];
}