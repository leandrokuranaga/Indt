using Proposal.Domain.SeedWork;
using IUnitOfWork = Proposal.Domain.SeedWork.IUnitOfWork;

namespace Proposal.Domain;

public interface IProposalRepository: IBaseRepository<Proposal>, IUnitOfWork
{
    
}