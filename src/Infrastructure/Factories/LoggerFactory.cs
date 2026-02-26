using Microsoft.Extensions.Logging;
namespace ApnaDhobi.Infrastructure.Factories;

public sealed class LoggerFactory
{
    public ILogger CreateLogger<T>() => new Microsoft.Extensions.Logging.LoggerFactory().CreateLogger<T>();

    public ILogger CreateLogger(Type type) => new Microsoft.Extensions.Logging.LoggerFactory().CreateLogger(type);
}