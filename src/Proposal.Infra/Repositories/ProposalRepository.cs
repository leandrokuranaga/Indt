using Proposal.Domain;
using Proposal.Infra.Repositories.Base;

namespace Proposal.Infra.Repositories;

public class ProposalRepository(Context context) : BaseRepository<Domain.Proposal.Proposal>(context), IProposalRepository
{
    
}
