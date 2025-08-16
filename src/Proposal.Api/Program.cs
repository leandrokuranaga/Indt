using System.Diagnostics.CodeAnalysis;
using Proposal.Api.Extensions;
using Proposal.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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