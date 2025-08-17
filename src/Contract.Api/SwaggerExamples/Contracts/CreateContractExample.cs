using Contract.Application.Contract.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Contract.Api.SwaggerExamples.Contracts
{
    public class CreateContractExample : IExamplesProvider<ContractRequest>
    {
        public ContractRequest GetExamples()
        {
            return new ContractRequest
            {
                Id = 1,
                InsuranceNameHolder = "John Doe",
                CPF = "12345678901",
                Money = 100.00m
            };
        }
    }
}
