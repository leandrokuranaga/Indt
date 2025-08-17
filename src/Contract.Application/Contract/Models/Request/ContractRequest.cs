namespace Contract.Application.Contract.Models.Request;

public record ContractRequest
{
    public int Id { get; set; }
    public string InsuranceNameHolder { get; set; }
    public string CPF { get; set; }
    public decimal Money { get; set; }
}