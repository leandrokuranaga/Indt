using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Models.Response;
using Contract.Application.Contract.Services;
using Contract.Domain.Contract.Enums;
using Contract.Domain.Contract.ValueObjects;
using Contract.Domain.SeedWork;
using ContractAggregate.Domain;
using Microsoft.Extensions.Logging;
using Moq;

namespace Contract.Unit.Tests._2._Application_Layer_Tests
    {
        public class ContractServiceTests
        {
            private readonly Mock<INotification> _notificationMock = new();
            private readonly Mock<IContractRepository> _repositoryMock = new();
            private readonly Mock<ILogger<ContractService>> _loggerMock = new();

            private ContractService CreateSut() =>
                new(_notificationMock.Object, _repositoryMock.Object, _loggerMock.Object);

        [Fact]
        public async Task ContractProposalAsync_ShouldPersistAndReturnMappedResponse()
        {
            // Arrange
            var request = new ContractRequest
            {
                Id = 123,
                InsuranceNameHolder = "Jane Doe",
                CPF = "07038612042",
                Money = 250
            };

            _repositoryMock
                .Setup(r => r.InsertOrUpdateAsync(It.IsAny<ContractAggregate.Domain.Contract>()))
                .ReturnsAsync((ContractAggregate.Domain.Contract c) => c);

            _repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            Mapster.TypeAdapterConfig.GlobalSettings.NewConfig<Money, decimal>()
                .MapWith(m => m.Value);

            var sut = CreateSut();

            // Act
            var result = await sut.ContractProposalAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ContractResponse>(result);
            Assert.Equal(request.Id, result.ProposalId);
            Assert.Equal(request.InsuranceNameHolder, result.InsuranceNameHolder);
            Assert.Equal("070.386.120-42", result.CPF);
            Assert.Equal(request.Money, result.MonthlyBill);

            _repositoryMock.Verify(r => r.InsertOrUpdateAsync(
                It.Is<ContractAggregate.Domain.Contract>(c =>
                    c.ProposalId == request.Id &&
                    c.InsuranceNameHolder == request.InsuranceNameHolder
                )), Times.Once);

            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _notificationMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Never);
        }

        [Fact]
        public async Task GetContractByIdAsync_ShouldReturnMappedResponse_WhenContractExists()
        {
            // Arrange
            var id = 42;
            var cpf = "67136373026";
            var bill = 500m;
            var holder = "John Wick";

            var domain = ContractAggregate.Domain.Contract.Create(
                proposalId: id,
                proposalStatus: ProposalStatusEnum.Approved,
                insuranceNameHolder: holder,
                cpf: new CPF(cpf),
                monthlyBill: new Money(bill),
                contractDate: DateTime.UtcNow
            );

            _repositoryMock
                .Setup(r => r.GetByIdAsync(id, false))
                .ReturnsAsync(domain);

            var sut = CreateSut();

            // Act
            var result = await sut.GetContractByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ContractResponse>(result);
            Assert.Equal(id, result.ProposalId);
            Assert.Equal(holder, result.InsuranceNameHolder);
            Assert.Equal(cpf, result.CPF);
            Assert.Equal(bill, result.MonthlyBill);

            _repositoryMock.Verify(r => r.GetByIdAsync(id, false), Times.Once);
            _notificationMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Never);
        }

        [Fact]
        public async Task GetContractByIdAsync_ShouldReturnNull_AndNotify_WhenContractNotFound()
        {
            // Arrange
            var id = 999;

            _repositoryMock
                .Setup(r => r.GetByIdAsync(id, false))
                .ReturnsAsync((ContractAggregate.Domain.Contract)null!);

            var sut = CreateSut();

            // Act
            var result = await sut.GetContractByIdAsync(id);

            // Assert
            Assert.Null(result);
            _repositoryMock.Verify(r => r.GetByIdAsync(id, false), Times.Once);
            _notificationMock.Verify(n =>
                n.AddNotification("Contract", "Contract not found", NotificationModel.ENotificationType.NotFound),
                Times.Once);
        }
    }
}
