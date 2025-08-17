using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Proposal.Api.Middlewares;

namespace Proposal.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class MvcConfigExtensions
{
    public static void AddCustomMvc(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
            options.ReturnHttpNotAcceptable = true;
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.Configure<RouteOptions>(options =>
        {
            options.ConstraintMap["lowercase"] = typeof(string);
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }
}