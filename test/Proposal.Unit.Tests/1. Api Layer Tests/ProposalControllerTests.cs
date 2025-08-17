using Microsoft.AspNetCore.Mvc;
using Moq;
using Proposal.Api.Controllers;
using Proposal.Application.Common;
using Proposal.Application.Proposal.Models.Request;
using Proposal.Application.Proposal.Models.Response;
using Proposal.Application.Proposal.Services;
using Proposal.Domain.Enums;
using Proposal.Domain.SeedWork;

namespace TestProject1._1._Api_Layer_Tests
{
    public class ProposalControllerTests
    {
        readonly Mock<IProposalService> mockProposalService;
        readonly ProposalController proposalController;
        readonly Mock<INotification> mockNotification;

        public ProposalControllerTests()
        {
            mockProposalService = new Mock<IProposalService>();
            mockNotification = new Mock<INotification>();
            proposalController = new ProposalController(mockProposalService.Object, mockNotification.Object);
        }

        [Fact]
        public async Task CreateProposalAsync_ShouldReturnOk_WhenProposalIsCreated()
        {
            // Arrange
            var request = new ProposalRequest
            {
                InsuranceType = EInsuranceType.Health,
                InsuranceNameHolder = "John Doe",
                CPF = "07038612042",
                MonthlyBill = 100.00m
            };

            var response = new ProposalResponse { Id = 1, CreationDate = DateTime.Now, ProposalStatus = EProposalStatus.InAnalysis, InsuranceType = EInsuranceType.Health };

            mockProposalService.Setup(s => s.CreateProposalAsync(request)).ReturnsAsync(response);

            // Act
            var result = await proposalController.CreateAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<ProposalResponse>>(okResult.Value);
            Assert.Equal(response, baseResponse.Data);
        }

        [Fact]
        public async Task UpdateProposalStatusAsync_ShouldReturnNoContent()
        {
            // Arrange
            int id = 1;

            var request = EProposalStatus.Approved;

            mockProposalService.Setup(s => s.UpdateProposalAsync(id, EProposalStatus.Approved))
                .ReturnsAsync(new BaseResponse<object>());

            // Act
            var result = await proposalController.UpdateAsync(id, request);

            // Assert
            var okResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetProposalsAsync_ShouldReturnOk()
        {
            var response = new List<ProposalResponse>
            {
                new() { Id = 1, CreationDate = DateTime.Now, ProposalStatus = EProposalStatus.InAnalysis, InsuranceType = EInsuranceType.Health },
                new() { Id = 2, CreationDate = DateTime.Now, ProposalStatus = EProposalStatus.Approved, InsuranceType = EInsuranceType.Car }
            };

            mockProposalService.Setup(s => s.GetProposalsAsync()).ReturnsAsync(response);

            // Act
            var result = await proposalController.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<List<ProposalResponse>>>(okResult.Value);
            Assert.Equal(response, baseResponse.Data);
        }

        [Fact]
        public async Task GetProposalAsync_ShouldReturnOk()
        {
            // Arrange
            int id = 1;

            var response = new ProposalResponse { Id = 1, CreationDate = DateTime.Now, ProposalStatus = EProposalStatus.InAnalysis, InsuranceType = EInsuranceType.Health };

            mockProposalService.Setup(s => s.GetProposalAsync(id)).ReturnsAsync(response);

            // Act
            var result = await proposalController.GetAsync(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<ProposalResponse>>(okResult.Value);
            Assert.Equal(response, baseResponse.Data);
        }
    }
}
