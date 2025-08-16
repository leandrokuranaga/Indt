using Contract.Domain.Contract;

namespace Contract.Infra.Repositories.Base;

public class ContractRepository(Context context) : BaseRepository<Domain.Contract.Contract>(context), IContractRepository
{

}