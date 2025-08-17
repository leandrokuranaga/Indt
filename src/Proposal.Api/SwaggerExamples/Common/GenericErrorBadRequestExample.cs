﻿using System.Diagnostics.CodeAnalysis;
using Proposal.Application.Common;
using Proposal.Domain.SeedWork;
using Swashbuckle.AspNetCore.Filters;

namespace Proposal.Api.SwaggerExamples.Commons;

[ExcludeFromCodeCoverage]
public class GenericErrorBadRequestExample : IExamplesProvider<BaseResponse<object>>
{
    public BaseResponse<object> GetExamples()
    {
        var notification = new NotificationModel
        {
            NotificationType = NotificationModel.ENotificationType.BadRequestError
        };

        notification.AddMessage("Field", "Field Required");

        notification.AddMessage("Error", "Generic validation error");

        return BaseResponse<object>.Fail(notification);
    }
}
