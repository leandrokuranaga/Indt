using Contract.Application.Contract.Models.Response;
using Mapster;

namespace Contract.Application.Map;

public static class ContractMapping
{
    private static bool _configured;

    public static void Register()
    {
        if (_configured) return;
        _configured = true;

        TypeAdapterConfig<Domain.Contract.Contract, ContractResponse>
            .NewConfig()
            .Map(d => d.ContractDate, s => s.ContractDate.Value)          
            .Map(d => d.ProposalStatus, s => s.ProposalStatus.ToString())
            .Map(d => d.CPF, s => s.CPF.Value)
            .Map(d => d.MonthlyBill, s => s.MonthlyBill.Value);
    }
}