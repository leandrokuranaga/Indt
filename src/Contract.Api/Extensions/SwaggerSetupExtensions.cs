using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Contract.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerSetupExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Contract API",
                Version = "v1",
                Description = "Contract API"
            });

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                var groupName = apiDesc.GroupName;
                return groupName == docName;
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            c.ExampleFilters();
            c.OperationFilter<SetApplicationJsonAsDefaultFilter>();
            c.EnableAnnotations();
            c.SchemaFilter<SuccessResponseSchemaFilter>();
        });
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
    }
    public static void UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contract API v1");
            c.RoutePrefix = "swagger";
        });
    }
}