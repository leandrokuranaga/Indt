using FluentValidation;
using Proposal.Application.Proposal.Models.Request;

namespace Proposal.Application.Validators.ProposalValidators;

public class CreateProposalRequestValidator : AbstractValidator<ProposalRequest>
{
    public CreateProposalRequestValidator()
    {
        RuleFor(x => x.InsuranceType)
            .IsInEnum()
            .WithMessage("Tipo de seguro inválido.");
    }
    
}