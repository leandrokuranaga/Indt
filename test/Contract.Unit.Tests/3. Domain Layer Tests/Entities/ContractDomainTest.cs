using Contract.Domain.Contract.Enums;
using Contract.Domain.Contract.ValueObjects;
using Contract.Domain.SeedWork.Exceptions;

namespace Contract.Unit.Tests._3._Domain_Layer_Tests.Entities
{
    public class ContractDomainTests
    {
        [Fact]
        public void Create_ShouldSucceed_WhenApprovedAndValidInputs()
        {
            // Arrange
            var proposalId = 123;
            var status = ProposalStatusEnum.Approved;
            var holder = "  John Doe  ";
            var cpf = new CPF("07038612042");
            var bill = new Money(149.90m);
            var fixedDate = new DateTime(2024, 01, 01, 00, 00, 00, DateTimeKind.Utc);

            // Act
            var contract = ContractAggregate.Domain.Contract.Create(
                proposalId: proposalId,
                proposalStatus: status,
                insuranceNameHolder: holder,
                cpf: cpf,
                monthlyBill: bill,
                contractDate: fixedDate
            );

            // Assert
            Assert.NotNull(contract);
            Assert.Equal(proposalId, contract.ProposalId);
            Assert.Equal(status, contract.ProposalStatus);
            Assert.Equal("John Doe", contract.InsuranceNameHolder);
            Assert.Equal(cpf, contract.CPF);
            Assert.Equal(bill, contract.MonthlyBill);
            Assert.Equal(fixedDate, contract.ContractDate.Value);
        }

        [Fact]
        public void Create_ShouldSetContractDateToUtcNow_WhenDateNotProvided()
        {
            // Arrange
            var before = DateTime.UtcNow.AddSeconds(-1);
            var cpf = new CPF("07038612042");
            var bill = new Money(100m);

            // Act
            var contract = ContractAggregate.Domain.Contract.Create(
                proposalId: 1,
                proposalStatus: ProposalStatusEnum.Approved,
                insuranceNameHolder: "Jane Doe",
                cpf: cpf,
                monthlyBill: bill
            );
            var after = DateTime.UtcNow.AddSeconds(1);

            // Assert
            Assert.True(contract.ContractDate.Value >= before && contract.ContractDate.Value <= after);
        }

        [Fact]
        public void Create_ShouldThrow_WhenProposalNotApproved()
        {
            // Arrange
            var cpf = new CPF("07038612042");
            var bill = new Money(100m);

            // Act
            var ex = Assert.Throws<BusinessRulesException>(() =>
                ContractAggregate.Domain.Contract.Create(
                    proposalId: 1,
                    proposalStatus: ProposalStatusEnum.InAnalysis,
                    insuranceNameHolder: "John Doe",
                    cpf: cpf,
                    monthlyBill: bill
                ));

            // Assert
            Assert.Contains("Proposal not approved to proceed", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_ShouldThrow_WhenInsuranceNameHolderIsMissing(string? holder)
        {
            // Arrange
            var cpf = new CPF("07038612042");
            var bill = new Money(100m);

            // Act
            var ex = Assert.Throws<BusinessRulesException>(() =>
                ContractAggregate.Domain.Contract.Create(
                    proposalId: 1,
                    proposalStatus: ProposalStatusEnum.Approved,
                    insuranceNameHolder: holder!,
                    cpf: cpf,
                    monthlyBill: bill
                ));

            // Assert
            Assert.Contains("Insurance name holder is mandatory", ex.Message);
        }

        [Fact]
        public void Create_ShouldThrow_WhenCpfIsNull()
        {
            // Arrange
            var bill = new Money(100m);

            // Act
            var ex = Assert.Throws<BusinessRulesException>(() =>
                ContractAggregate.Domain.Contract.Create(
                    proposalId: 1,
                    proposalStatus: ProposalStatusEnum.Approved,
                    insuranceNameHolder: "John Doe",
                    cpf: null!,
                    monthlyBill: bill
                ));

            // Assert
            Assert.Contains("Invalid CPF", ex.Message);
        }

        [Fact]
        public void Create_ShouldThrow_WhenCpfIsInvalid()
        {
            // Arrange
            var invalidCpf = new CPF("12345678900");
            var bill = new Money(100m);

            // Act
            var ex = Assert.Throws<BusinessRulesException>(() =>
                ContractAggregate.Domain.Contract.Create(
                    proposalId: 1,
                    proposalStatus: ProposalStatusEnum.Approved,
                    insuranceNameHolder: "John Doe",
                    cpf: invalidCpf,
                    monthlyBill: bill
                ));

            // Assert
            Assert.Contains("Invalid CPF", ex.Message);
        }
    }
}