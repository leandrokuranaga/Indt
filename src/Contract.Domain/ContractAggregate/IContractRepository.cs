using Contract.Domain.SeedWork;

namespace Contract.Domain.Contract;

public interface IContractRepository : IBaseRepository<Contract>, IUnitOfWork
{
    
}