using Proposal.Domain.SeedWork;

namespace Proposal.Domain.OutboxAggregate
{
    public interface IOutboxRepository : IBaseRepository<Outbox>, IUnitOfWork
    {
    }
}
