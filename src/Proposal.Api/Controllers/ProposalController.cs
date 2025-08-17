using System.Net;
using Microsoft.AspNetCore.Mvc;
using Proposal.Api.SwaggerExamples.Commons;
using Proposal.Application.Common;
using Proposal.Application.Proposal.Models.Request;
using Proposal.Application.Proposal.Models.Response;
using Proposal.Application.Proposal.Services;
using Proposal.Domain.Enums;
using Proposal.Domain.SeedWork;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Proposal.Api.Controllers;

/// <summary>
/// Controller used to manage proposals operations, such as creation, retrieval and management
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProposalController(
    IProposalService proposalService,
    INotification notification
) 
    : BaseController(notification)
{
    /// <summary>
    /// Create a new proposal in the system.
    /// </summary>
    /// <param name="request">The insurance type required to create a new entry in the system.</param>
    /// <returns>A response containing the created proposal, or an error message if the input is invalid.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new proposal in the system.",
        Description = "Create a new proposal in the system."
    )]
    [ProducesResponseType(typeof(BaseResponse<ProposalResponse>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GenericErrorBadRequestExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status409Conflict)]
    [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(GenericErrorConflictExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
    public async Task<IActionResult> CreateAsync([FromBody] ProposalRequest request)
    {
        var result = await proposalService.CreateProposalAsync(request);
        return Response(BaseResponse<ProposalResponse>.Ok(result));
    }
    
    /// <summary>
    /// Update proposal status in the system.
    /// </summary>
    /// <param name="id">The ID of the proposal to be updated (must be greater than 0).</param>
    /// <param name="request">The status required to update a proposal in the system.</param>
    /// <returns>A response containing the proposal, or an error message if the input is invalid.</returns>
    [HttpPatch("{id:int:min(1)}")]
    [SwaggerOperation(
        Summary = "Update proposal status in the system.",
        Description = "Update a status from a proposal in the system."
    )]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GenericErrorBadRequestExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status409Conflict)]
    [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(GenericErrorConflictExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] EProposalStatus request)
    {
        await proposalService.UpdateProposalAsync(id, request);
        return Response(BaseResponse<ProposalResponse>.Ok(null));
    }
    
    /// <summary>
    /// Get all proposals
    /// </summary>
    /// <returns>A response containing the proposals.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Proposals",
        Description = "Get all proposals availables in the system."
    )]
    [ProducesResponseType(typeof(BaseResponse<List<ProposalResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GenericErrorBadRequestExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status409Conflict)]
    [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(GenericErrorConflictExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await proposalService.GetProposalsAsync();
        return Response(BaseResponse<List<ProposalResponse>>.Ok(result));
    }
    
    /// <summary>
    /// Get proposal by id
    /// </summary>
    /// <returns>A response containing the proposal.</returns>
    [HttpGet("{id:int:min(1)}")]
    [SwaggerOperation(
        Summary = "Get Proposal",
        Description = "Get proposal by id available in the system."
    )]
    [ProducesResponseType(typeof(BaseResponse<ProposalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GenericErrorBadRequestExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status409Conflict)]
    [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(GenericErrorConflictExample))]
    [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await proposalService.GetProposalAsync(id);
        return Response(BaseResponse<ProposalResponse>.Ok(result));
    }
}