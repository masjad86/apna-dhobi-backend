
using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ApnaDhobi.Infrastructure.Abstractions;

public class AbstractQueryHandler<TQuery, TResponse>(
    IRunner runner, 
    IPerformanceMonitor monitor,
    ILogger<AbstractQueryHandler<TQuery, TResponse>>? logger = null,
    HandlerSetting? settings = null) : IRequestHandler<TQuery, TResponse>
    where TQuery : ICommand<TResponse>
{
    public virtual async Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
    {
        if (settings?.EnableMonitoring == true)
        {
            monitor.Start();
            try
            {
                return await runner.RunAsync(request, cancellationToken);
            }
            finally
            {
                monitor.Stop();
                logger?.LogInformation("Query {QueryName} in {ElapsedMilliseconds} ms", typeof(TQuery).Name, monitor.ElapsedMilliseconds);
            }
        }
        return await runner.RunAsync(request, cancellationToken);
    }
}
