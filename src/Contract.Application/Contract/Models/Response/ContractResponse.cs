namespace Contract.Application.Contract.Models.Response;

public class ContractResponse
{
    public int Id { get; set; }
    public DateTime ContractDate { get; set; }
    public int ProposalId { get; set; }
    public string InsuranceNameHolder { get; set; } = string.Empty;
    public string ProposalStatus { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public decimal MonthlyBill { get; set; }


    public static explicit operator ContractResponse(ContractAggregate.Domain.Contract contract)
    {
        return new ContractResponse
        {
            Id = contract.Id,
            ContractDate = contract.ContractDate.Value,
            ProposalId = contract.ProposalId,
            InsuranceNameHolder = contract.InsuranceNameHolder,
            ProposalStatus = contract.ProposalStatus.ToString(),
            CPF = contract.CPF.Value,
            MonthlyBill = contract.MonthlyBill.Value
        };
    }
}