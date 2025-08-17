using System.Diagnostics.CodeAnalysis;
using Proposal.Application.Common;
using Proposal.Domain.SeedWork;
using Swashbuckle.AspNetCore.Filters;

namespace Proposal.Api.SwaggerExamples.Commons;

[ExcludeFromCodeCoverage]
public class GenericErrorConflictExample : IExamplesProvider<BaseResponse<object>>
{
    public BaseResponse<object> GetExamples()
    {
        var notification = new NotificationModel
        {
            NotificationType = NotificationModel.ENotificationType.BusinessRules
        };

        notification.AddMessage("Field", "Field already in use");
        notification.AddMessage("Conflict", "This resource already exists and cannot be duplicated");

        return BaseResponse<object>.Fail(notification);
    }
}