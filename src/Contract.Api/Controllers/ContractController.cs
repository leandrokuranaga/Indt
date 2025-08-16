using Contract.Api.SwaggerExamples.Common;
using Contract.Application.Common;
using Contract.Application.Contract.Models.Request;
using Contract.Application.Contract.Models.Response;
using Contract.Application.Contract.Services;
using Contract.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Contract.Api.Controllers;

public class ContractController(
    IContractService contractService,
    INotification notification
    ) 
    : BaseController(notification)
{
    /// <summary>
    /// Contract a new proposal (only if its approved)
    /// </summary>
    /// <param name="request">The game data required to create a new entry in the system.</param>
    /// <returns>A response containing the created game, or an error message if the input is invalid.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Contract a new proposal",
        Description = "Contract a new proposal (only if its approved)",
        OperationId = "Contract.Create",
        Tags = new[] { "Contract" }
    )]
    [ProducesResponseType(typeof(BaseResponse<ContractResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GenericErrorBadRequestExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status409Conflict)]
    [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(GenericErrorConflictExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
    public async Task<IActionResult> Create([FromBody] ContractRequest request)
    {
        var result = await contractService.ContractProposalAsync(request);
        return Response(BaseResponse<ContractResponse>.Ok(result));
    }
}