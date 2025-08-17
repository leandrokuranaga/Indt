namespace Contract.Application.Contract.Models.Response;

public record ContractResponse
{
    public DateTime ContractDate { get; init; }
    public int ProposalId { get; init; }
    public string InsuranceNameHolder { get; init; } = string.Empty;
    public string ProposalStatus { get; init; } = string.Empty;
    public string CPF { get; init; } = string.Empty;
    public decimal MonthlyBill { get; init; }
}