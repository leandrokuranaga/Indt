using Contract.Domain.Contract;
using ContractAggregate.Domain;

namespace Contract.Infra.Repositories.Base;

public class ContractRepository(Context context) : BaseRepository<ContractAggregate.Domain.Contract>(context), IContractRepository
{

}