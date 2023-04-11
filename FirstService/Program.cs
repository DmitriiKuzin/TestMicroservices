using Common.Dto;
using FirstService.Validation;
using FluentValidation;
using MassTransit;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IValidator<UserDto>, UserDtoValidator>();
builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((registrationContext, factoryConfigurator) =>
    {
        factoryConfigurator.Host(registrationContext.GetService<IConfiguration>()["MQ_HOST"]);
        factoryConfigurator.ConfigureEndpoints(registrationContext);
    });
});

var app = builder.Build();

var loggera = app.Services.GetService<ILoggerFactory>().CreateLogger<Program>();
loggera.LogInformation("Отправлено сообщение {@Message}", new  UserDto()
{
    FirstName = "asdaf"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapPost("api/User", async (UserDto userDto, HttpContext context) =>
{
    var validator = context.RequestServices.GetService<IValidator<UserDto>>();
    var validationResult = validator.Validate(userDto);
    if (!validationResult.IsValid)
    {
        return
            Results.BadRequest(
                validationResult
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .ToList());
    }
    
    var bus = context.RequestServices.GetRequiredService<IBus>();
    await bus.Publish(userDto);
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
    logger.LogInformation("Отправлено сообщение {@Message}", userDto);
    return Results.Ok();
});

app.Run();