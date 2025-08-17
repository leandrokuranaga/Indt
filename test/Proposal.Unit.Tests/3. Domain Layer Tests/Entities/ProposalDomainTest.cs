using Proposal.Domain.Enums;
using Proposal.Domain.Proposal.Proposal.ValueObjects;
using Proposal.Domain.ProposalAggregate.ValueObjects;

namespace TestProject1._3._Domain_Layer_Tests.Entities
{
    public class ProposalDomainTest
    {
        [Fact]
        public void ProposalDomainSuccess()
        {
            #region Arrange

            var mockProposal = new Proposal.Domain.ProposalAggregate.Proposal(
                new UtcDate(DateTime.SpecifyKind(new DateTime(2023, 04, 01), DateTimeKind.Utc)),
                EProposalStatus.InAnalysis,
                EInsuranceType.Health,
                "John Doe",
                new CPF("12345678901"),
                new Money(1000.00m)
 
            );

            #endregion

            #region Act
            var mockProposalDomainAct = new Proposal.Domain.ProposalAggregate.Proposal(
                mockProposal.CreationDate,
                mockProposal.ProposalStatus,
                mockProposal.InsuranceType,
                mockProposal.InsuranceNameHolder,
                mockProposal.CPF,
                mockProposal.MonthlyBill
            );

            #endregion

            #region Assert

            Assert.NotNull(mockProposalDomainAct);
            Assert.Equal(mockProposal.CreationDate, mockProposalDomainAct.CreationDate);
            Assert.Equal(mockProposal.ProposalStatus, mockProposalDomainAct.ProposalStatus);
            Assert.Equal(mockProposal.InsuranceType, mockProposalDomainAct.InsuranceType);
            Assert.Equal(mockProposal.InsuranceNameHolder, mockProposalDomainAct.InsuranceNameHolder);
            Assert.Equal(mockProposal.CPF, mockProposalDomainAct.CPF);
            Assert.Equal(mockProposal.MonthlyBill, mockProposalDomainAct.MonthlyBill);

            #endregion
        }

        [Fact]
        public void ProposalCreateSuccess()
        {
            #region Arrange
            var insuranceType = EInsuranceType.Health;
            var insuranceNameHolder = "John Doe";
            var cPF = new CPF("67136373026");
            var monthlyBill = new Money(1000.00m);
            #endregion

            #region Act
            var proposal = Proposal.Domain.ProposalAggregate.Proposal.Create(
                insuranceType,
                insuranceNameHolder,
                cPF,
                monthlyBill
            );
            #endregion

            #region Assert
            Assert.NotNull(proposal);
            Assert.Equal(EProposalStatus.InAnalysis, proposal.ProposalStatus);
            Assert.Equal(insuranceType, proposal.InsuranceType);
            Assert.Equal(insuranceNameHolder.Trim(), proposal.InsuranceNameHolder);
            Assert.Equal(cPF, proposal.CPF);
            Assert.Equal(monthlyBill, proposal.MonthlyBill);
            #endregion
        }

        [Fact]
        public void ProposalUpdateStatusSuccess()
        {
            #region Arrange
            var proposal = Proposal.Domain.ProposalAggregate.Proposal.Create(
                EInsuranceType.Health,
                "John Doe",
                new CPF("67136373026"),
                new Money(1000.00m)
            );
            #endregion

            #region Act
            proposal.SetStatus(EProposalStatus.Approved);
            #endregion

            #region Assert
            Assert.Equal(EProposalStatus.Approved, proposal.ProposalStatus);
            #endregion
        }
    }
}
