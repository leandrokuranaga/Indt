using Proposal.Domain.OutboxAggregate;
using Proposal.Infra.Repositories.Base;

namespace Proposal.Infra.Repositories
{
    public class OutboxRepository(Context context) : BaseRepository<Outbox>(context), IOutboxRepository
    {
    }
}
