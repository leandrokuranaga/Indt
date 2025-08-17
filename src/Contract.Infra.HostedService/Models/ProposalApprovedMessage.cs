namespace Contract.Infra.MessageBus.Models
{
    public sealed record ProposalApprovedMessage
    {
        public int ProposalId { get; init; }
        public string InsuranceNameHolder { get; init; } = string.Empty;
        public string CPF { get; init; } = string.Empty;
        public decimal MonthlyBill { get; init; }
    }
}
