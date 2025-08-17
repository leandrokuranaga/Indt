using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Logging;
using Moq;
using Proposal.Application.Common;
using Proposal.Application.Proposal.Models.Request;
using Proposal.Application.Proposal.Models.Response;
using Proposal.Application.Proposal.Services;
using Proposal.Domain;
using Proposal.Domain.Enums;
using Proposal.Domain.OutboxAggregate;
using Proposal.Domain.ProposalAggregate.ValueObjects;
using Proposal.Domain.SeedWork;
using Xunit;
using ProposalAggregate = Proposal.Domain.ProposalAggregate;

namespace TestProject1._2._Application_Layer_Tests
{
    public class ProposalServiceTests
    {
        private readonly Mock<INotification> _notificationMock = new();
        private readonly Mock<IProposalRepository> _proposalRepoMock = new();
        private readonly Mock<IOutboxRepository> _outboxRepoMock = new();
        private readonly Mock<IUnitOfWork> _uowMock = new();
        private readonly Mock<ILogger<ProposalService>> _loggerMock = new();

        private ProposalService CreateSut() =>
            new(_notificationMock.Object, _proposalRepoMock.Object, _outboxRepoMock.Object, _loggerMock.Object, _uowMock.Object);

        [Fact]
        public async Task CreateProposalAsync_ShouldPersistAndReturnMappedResponse()
        {
            // Arrange
            var request = new ProposalRequest
            {
                InsuranceType = EInsuranceType.Health,
                InsuranceNameHolder = "John Doe",
                CPF = "07038612042",
                MonthlyBill = 123.45m
            };

            _proposalRepoMock
                .Setup(r => r.InsertOrUpdateAsync(It.IsAny<ProposalAggregate.Proposal>()))
                .ReturnsAsync((ProposalAggregate.Proposal p) => p);

            _proposalRepoMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var sut = CreateSut();

            // Act
            var result = await sut.CreateProposalAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProposalResponse>(result);
            Assert.Equal(request.InsuranceType, result.InsuranceType);

            _proposalRepoMock.Verify(r => r.InsertOrUpdateAsync(
                It.Is<ProposalAggregate.Proposal>(p =>
                    p.InsuranceType == request.InsuranceType &&
                    p.InsuranceNameHolder == request.InsuranceNameHolder &&
                    p.CPF.Value == CPF.Create(request.CPF).Value &&
                    p.MonthlyBill.Value == request.MonthlyBill
                )), Times.Once);

            _proposalRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _notificationMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Never);
        }

        [Fact]
        public async Task UpdateProposalAsync_WhenNotFound_ShouldNotifyAndReturnNull()
        {
            // Arrange
            var id = 999;
            _proposalRepoMock.Setup(r => r.GetByIdAsync(id, false)).ReturnsAsync((ProposalAggregate.Proposal)null!);

            var sut = CreateSut();

            // Act
            var result = await sut.UpdateProposalAsync(id, EProposalStatus.Refused);

            // Assert
            Assert.Null(result);
            _notificationMock.Verify(n => n.AddNotification("Proposal", "Proposal not found", NotificationModel.ENotificationType.NotFound), Times.Once);
            _uowMock.Verify(u => u.BeginTransactionAsync(), Times.Never);
            _outboxRepoMock.Verify(o => o.InsertOrUpdateAsync(It.IsAny<Outbox>()), Times.Never);
        }

        [Fact]
        public async Task UpdateProposalAsync_WithExistingProposal_ShouldUpdateAndCommit_WithoutOutbox_WhenNotApproved()
        {
            // Arrange
            var id = 1;
            var existing = ProposalAggregate.Proposal.Create(EInsuranceType.Car, "Jane Doe", new CPF("07038612042"), new Money(200m));

            _proposalRepoMock.Setup(r => r.GetByIdAsync(id, false)).ReturnsAsync(existing);
            _proposalRepoMock.Setup(r => r.UpdateAsync(It.IsAny<ProposalAggregate.Proposal>())).Returns(Task.CompletedTask);
            _proposalRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

            var sut = CreateSut();

            // Act
            var response = await sut.UpdateProposalAsync(id, EProposalStatus.Refused);

            // Assert
            Assert.NotNull(response);

            _uowMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _proposalRepoMock.Verify(r => r.UpdateAsync(It.Is<ProposalAggregate.Proposal>(p => p.ProposalStatus == EProposalStatus.Refused)), Times.Once);
            _proposalRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _outboxRepoMock.Verify(o => o.InsertOrUpdateAsync(It.IsAny<Outbox>()), Times.Never);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProposalAsync_WithApprovedStatus_ShouldWriteOutboxAndCommit()
        {
            // Arrange
            var id = 7;
            var existing = ProposalAggregate.Proposal.Create(EInsuranceType.Health, "John Wick", new CPF("67136373026"), new Money(500m));

            _proposalRepoMock.Setup(r => r.GetByIdAsync(id, false)).ReturnsAsync(existing);
            _proposalRepoMock.Setup(r => r.UpdateAsync(It.IsAny<ProposalAggregate.Proposal>())).Returns(Task.CompletedTask);
            _proposalRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _outboxRepoMock.Setup(o => o.InsertOrUpdateAsync(It.IsAny<Outbox>())).ReturnsAsync((Outbox o) => o);
            _outboxRepoMock.Setup(o => o.SaveChangesAsync()).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _uowMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

            var sut = CreateSut();

            // Act
            var response = await sut.UpdateProposalAsync(id, EProposalStatus.Approved);

            // Assert
            Assert.NotNull(response);

            _proposalRepoMock.Verify(r => r.UpdateAsync(It.Is<ProposalAggregate.Proposal>(p => p.ProposalStatus == EProposalStatus.Approved)), Times.Once);
            _outboxRepoMock.Verify(o => o.InsertOrUpdateAsync(It.Is<Outbox>(ob =>
                ob.Type == "plain-json" &&
                !string.IsNullOrWhiteSpace(ob.Content) &&
                ob.Content.Contains("\"type\":\"object\"") &&
                ob.Content.Contains("\"version\":1") &&
                ob.Content.Contains("\"ProposalId\"") &&
                ob.Content.Contains("\"InsuranceNameHolder\"") &&
                ob.Content.Contains("\"CPF\"") &&
                ob.Content.Contains("\"MonthlyBill\"")
            )), Times.Once);

            _outboxRepoMock.Verify(o => o.SaveChangesAsync(), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task GetProposalsAsync_ShouldReturnMappedList()
        {
            // Arrange
            var list = new List<ProposalAggregate.Proposal>
            {
                ProposalAggregate.Proposal.Create(EInsuranceType.Health, "A", new CPF("07038612042"), new Money(100m)),
                ProposalAggregate.Proposal.Create(EInsuranceType.Car, "B", new CPF("67136373026"), new Money(200m))
            };

            _proposalRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var sut = CreateSut();

            // Act
            var result = await sut.GetProposalsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, x => x.InsuranceType == EInsuranceType.Health);
            Assert.Contains(result, x => x.InsuranceType == EInsuranceType.Car);
        }

        [Fact]
        public async Task GetProposalAsync_WhenExists_ShouldReturnMappedResponse()
        {
            // Arrange
            var id = 10;
            var domain = ProposalAggregate.Proposal.Create(EInsuranceType.Life, "Holder", new CPF("07038612042"), new Money(300m));

            _proposalRepoMock.Setup(r => r.GetByIdAsync(id, true)).ReturnsAsync(domain);

            var sut = CreateSut();

            // Act
            var result = await sut.GetProposalAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(EInsuranceType.Life, result.InsuranceType);
        }

        [Fact]
        public async Task GetProposalAsync_WhenNotFound_ShouldNotifyAndReturnNull()
        {
            // Arrange
            var id = 404;
            _proposalRepoMock.Setup(r => r.GetByIdAsync(id, true)).ReturnsAsync((ProposalAggregate.Proposal)null!);

            var sut = CreateSut();

            // Act
            var result = await sut.GetProposalAsync(id);

            // Assert
            Assert.Null(result);
            _notificationMock.Verify(n => n.AddNotification("Proposal", "Proposal not found", NotificationModel.ENotificationType.NotFound), Times.Once);
        }
    }
}