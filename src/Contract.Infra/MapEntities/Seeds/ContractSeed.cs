namespace Contract.Infra.MapEntities.Seeds;

using Contract.Domain.Contract.Enums;
using Contract.Domain.ContractAggregate.ValueObjects;

public static class ContractSeed
{
    public static IEnumerable<object> RootContracts() => new[]
    {
        new
        {
            Id = 1,
            ContractDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 04, 01), DateTimeKind.Utc)),
            ProposalId = 1,
            InsuranceNameHolder = "João da Silva",
            ProposalStatus = ProposalStatusEnum.Approved
        },
        new
        {
            Id = 2,
            ContractDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 04, 01), DateTimeKind.Utc)),
            ProposalId = 2,
            InsuranceNameHolder = "Maria Oliveira",
            ProposalStatus = ProposalStatusEnum.Approved
        },
        new
        {
            Id = 3,
            ContractDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 04, 01), DateTimeKind.Utc)),
            ProposalId = 3,
            InsuranceNameHolder = "Carlos Pereira",
            ProposalStatus = ProposalStatusEnum.Approved
        },
        new
        {
            Id = 4,
            ContractDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 04, 01), DateTimeKind.Utc)),
            ProposalId = 4,
            InsuranceNameHolder = "Ana Souza",
            ProposalStatus = ProposalStatusEnum.Approved
        },
        new
        {
            Id = 5,
            ContractDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 04, 01), DateTimeKind.Utc)),
            ProposalId = 5,
            InsuranceNameHolder = "Pedro Santos",
            ProposalStatus = ProposalStatusEnum.Approved
        }
    };

    public static IEnumerable<object> CpfOwned() => new[]
    {
        new { ContractId = 1, Value = "07038612042" },
        new { ContractId = 2, Value = "20791888010" },
        new { ContractId = 3, Value = "14590488060" },
        new { ContractId = 4, Value = "17448756001" },
        new { ContractId = 5, Value = "68456412007" }
    };

    public static IEnumerable<object> MonthlyBillOwned() => new[]
    {
        new { ContractId = 1, Value = 149.90m, Currency = "BRL" },
        new { ContractId = 2, Value = 199.50m, Currency = "BRL" },
        new { ContractId = 3, Value = 99.00m,  Currency = "BRL" },
        new { ContractId = 4, Value = 250.00m, Currency = "BRL" },
        new { ContractId = 5, Value = 300.75m, Currency = "BRL" }
    };
}