using Contract.Api.Controllers;
using Contract.Application.Common;
using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Models.Response;
using Contract.Application.Contract.Services;
using Contract.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Contract.Unit.Tests._1._Api_Layer_Tests
{
    public class ContractControllerTests
    {
        readonly Mock<IContractService> mockContractService;
        readonly ContractController contractController;
        readonly Mock<INotification> mockNotification;

        public ContractControllerTests()
        {
            mockContractService = new Mock<IContractService>();
            mockNotification = new Mock<INotification>();
            contractController = new ContractController(mockContractService.Object, mockNotification.Object);
        }

        [Fact]
        public async Task CreateContractAsync_ShouldReturnOk()
        {
            // Arrange
            var request = new ContractRequest
            {
                Id = 1,
                Money = 100.00m,
                InsuranceNameHolder = "John Doe",
                CPF = "07038612042"
            };

            var response = new ContractResponse
            {
                Id = 1,
                ContractDate = DateTime.Now,
                InsuranceNameHolder = "John Doe",
                CPF = "07038612042",
                MonthlyBill = 100.00m
            };


            mockContractService.Setup(s => s.ContractProposalAsync(request)).ReturnsAsync(response);

            // Act
            var result = await contractController.Create(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<ContractResponse>>(okResult.Value);
            Assert.Equal(response, baseResponse.Data);
        }

        [Fact]
        public async Task GetContractByIdAsync_ShouldReturnOk()
        {
            // Arrange
            int id = 1;

            var response = new ContractResponse
            {
                Id = 1,
                ContractDate = DateTime.Now,
                InsuranceNameHolder = "John Doe",
                CPF = "07038612042",
                MonthlyBill = 100.00m
            };


            mockContractService.Setup(s => s.GetContractByIdAsync(id)).ReturnsAsync(response);

            // Act
            var result = await contractController.GetAsync(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<ContractResponse>>(okResult.Value);
            Assert.Equal(response, baseResponse.Data);
        }
    }
}
