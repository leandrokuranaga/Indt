using Proposal.Domain.Enums;

namespace Proposal.Infra.MapEntities.Seeds;

public static class ProposalSeed
{
    public static List<Domain.Proposal.Proposal> Proposals()
    {
        var creationDate1 = new DateTime(2023, 10, 1);
        var creationDate2 = creationDate1.AddDays(7);
        
        return
        [
            new Domain.Proposal
            {
                Id = 1,
                CreationDate = creationDate1,
                ProposalStatus = EProposalStatus.Approved,
                InsuranceType = EInsuranceType.Life,
            },
            
            new Domain.Proposal
            {
                Id = 2,
                CreationDate = creationDate2,
                ProposalStatus = EProposalStatus.Approved,
                InsuranceType = EInsuranceType.Health,
            }
        ];
    }
}