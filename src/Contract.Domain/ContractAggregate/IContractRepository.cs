
using Contract.Domain.SeedWork;

namespace ContractAggregate.Domain;

public interface IContractRepository : IBaseRepository<Contract>, IUnitOfWork
{
    
}