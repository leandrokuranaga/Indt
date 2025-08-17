using System.Diagnostics.CodeAnalysis;
using Contract.Application.Common;
using Contract.Domain.SeedWork;
using Swashbuckle.AspNetCore.Filters;

namespace Proposal.Api.SwaggerExamples.Commons;

[ExcludeFromCodeCoverage]
public class GenericErrorNotFoundExample : IExamplesProvider<BaseResponse<object>>
{
    public BaseResponse<object> GetExamples()
    {
        var notification = new NotificationModel
        {
            NotificationType = NotificationModel.ENotificationType.NotFound
        };

        notification.AddMessage("Field", "Resource not found");
        notification.AddMessage("NotFound", "The requested resource does not exist");

        return BaseResponse<object>.Fail(notification);
    }
}
