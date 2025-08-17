using System.Diagnostics.CodeAnalysis;
using Contract.Api.Extensions;
using Contract.Api.Middlewares;
using Contract.Application;
using Contract.Infra;
using Contract.Infra.MessageBus.Services;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Retry.Simple;     // .SimpleRetryStrategy(...)
using Rebus.ServiceProvider;  // services.AddRebus(...)
using Rebus.RabbitMq;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddGlobalCorsPolicy();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddInfraModuleDependency();
builder.Services.AddApplicationModule();

var rabbitMqConn = builder.Configuration.GetConnectionString("RabbitMq");

builder.Services.AddRebus(cfg => cfg
    .Transport(t => t.UseRabbitMq(rabbitMqConn, inputQueueName: "proposal-queue"))
    .Options(o => o.RetryStrategy("contract-error", maxDeliveryAttempts: 1)));

builder.Services.AutoRegisterHandlersFromAssemblyOf<ProposalApprovedMessageHandler>();


builder.Services.AddCustomMvc();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }