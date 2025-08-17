using Abp.Domain.Entities;
using Contract.Domain.Contract.Enums;
using Contract.Domain.Contract.ValueObjects;
using Contract.Domain.SeedWork.Exceptions;
using IAggregateRoot = Contract.Domain.SeedWork.IAggregateRoot;

namespace Contract.Domain.Contract;

public class Contract : Entity, IAggregateRoot
{
    public DateTime ContractDate { get; set; }
    public int ProposalId { get; set; }
    public string InsuranceNameHolder { get; set; }
    public string ProposalStatus { get; set; }
    public CPF CPF { get; set; }
    public Money MonthlyBill { get; set; }
    
    public Contract()
    {
        
    }
    
    public Contract(
        DateTime contractDate,
        int proposalId,
        string insuranceNameHolder,
        CPF cpf,
        Money monthlyBill
        )
    {
        ContractDate = contractDate;
        ProposalId = proposalId;
        InsuranceNameHolder = insuranceNameHolder;
        CPF = cpf;
        MonthlyBill = monthlyBill;
    }
    
    public static Contract Create(
        int proposalId,
        string proposalStatus,
        string insuranceNameHolder,
        CPF cpf,
        Money monthlyBill,
        DateTime? contractDate = null)
    {
        if (proposalStatus != ProposalStatusEnum.Approved.ToString())
            throw new BusinessRulesException("Proposal not approved to proceed");

        if (string.IsNullOrWhiteSpace(insuranceNameHolder))
            throw new BusinessRulesException("Insurance name holder is mandatory");

        if (cpf is null || !cpf.IsValid)
            throw new BusinessRulesException("Invalid CPF");

        var contract = new Contract(
            contractDate ?? DateTime.UtcNow,
            proposalId,
            insuranceNameHolder.Trim(),
            cpf,
            monthlyBill);

        contract.ProposalStatus = proposalStatus;
        
        return contract;
    }
    
    
}