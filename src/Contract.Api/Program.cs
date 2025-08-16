using System.Diagnostics.CodeAnalysis;
using Contract.Api.Extensions;
using Contract.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomMvc();

builder.Services.AddGlobalCorsPolicy();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }