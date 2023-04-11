using System.Runtime.CompilerServices;
using DAL;
using MassTransit;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SecondService;
using SecondService.Commands;
using SecondService.Consumers;
using SecondService.Requests;
using Serilog;

[assembly: MediatorOptions(ServiceLifetime = ServiceLifetime.Scoped)]
[assembly: InternalsVisibleTo("SecondService.Tests")]

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator();
builder.Services.AddDbContext<Context>();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
builder.Services.AddScoped<LoggingConsumeObserver>();
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumeObserver<LoggingConsumeObserver>();
    configurator.UsingRabbitMq((registrationContext, factoryConfigurator) =>
    {
        factoryConfigurator.Host(registrationContext.GetRequiredService<IConfiguration>()["MQ_HOST"]);
        factoryConfigurator.ConfigureEndpoints(registrationContext);
    });
    configurator.AddConsumer<CreateUserConsumer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapPost("api/Users", async (GetUsers request, HttpContext context) =>
{
    var mediator = context.RequestServices.GetRequiredService<IMediator>();
    return await mediator.Send(request);
});

app.MapPost("api/BindUserToOrganization", async (BindUserToOrganization command, HttpContext context) =>
{
    var mediator = context.RequestServices.GetRequiredService<IMediator>();
    return await mediator.Send(command);
});

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<Context>();
context.Database.Migrate();
app.Run();
