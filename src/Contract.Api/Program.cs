using System.Diagnostics.CodeAnalysis;
using Contract.Api.Extensions;
using Contract.Api.Middlewares;
using Contract.Application;
using Contract.Infra;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Routing.TypeBased;

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

builder.Services.AddRebus(configure => configure
    .Transport(t => t.UseRabbitMq(rabbitMqConn, "proposal-queue"))
    .Routing(r => r.TypeBased())
    .Options(o =>
    {
        o.SetNumberOfWorkers(1);
        o.SetMaxParallelism(4);
    })
);

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