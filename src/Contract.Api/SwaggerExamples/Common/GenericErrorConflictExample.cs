using System.Diagnostics.CodeAnalysis;
using Contract.Application.Common;
using Contract.Domain.SeedWork;
using Swashbuckle.AspNetCore.Filters;

namespace Contract.Api.SwaggerExamples.Common;

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