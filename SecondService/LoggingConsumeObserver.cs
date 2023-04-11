using MassTransit;

namespace SecondService;

public class LoggingConsumeObserver: IConsumeObserver
{
    private readonly ILogger<LoggingConsumeObserver> _logger;

    public LoggingConsumeObserver(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<LoggingConsumeObserver>();
    }

    public Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        _logger.LogInformation("Получено сообщение {@Message}", context.Message);
        return Task.CompletedTask;
    }

    public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }
}