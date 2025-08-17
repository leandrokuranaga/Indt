using Microsoft.EntityFrameworkCore;
using Proposal.Api.Extensions;
using Proposal.Api.Middlewares;
using Proposal.Application;
using Proposal.Infra;
using Proposal.Infra.HostedService;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddHostedService<OutboxProcessorService>();

builder.Services.AddRebus(configure => configure
    .Transport(t => t.UseRabbitMqAsOneWayClient(
        builder.Configuration.GetConnectionString("Rabbitmq")))
    .Routing(r => r.TypeBased()
        .MapFallback("proposal-queue")));

builder.Services.AddGlobalCorsPolicy();
builder.Services.AddCustomMvc();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddInfraModuleDependency();
builder.Services.AddApplicationModule();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.MapControllers();


app.UseDeveloperExceptionPage();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }