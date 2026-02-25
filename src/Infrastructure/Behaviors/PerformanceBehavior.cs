using ApnaDhobi.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using MediatR;

namespace ApnaDhobi.Infrastructure.Behaviors;

/// <summary>
/// Pipeline behavior for monitoring the performance of requests. This class implements the IPipelineBehavior interface and uses an IPerformanceMonitor to track the execution time of requests. It logs a warning if the execution time exceeds a specified threshold, allowing to identify potential performance issues in the application. Otherwise, it logs an informational message with the execution time for successful requests.
/// </summary>
/// <typeparam name="TRequest">Request type being monitored.</typeparam>
/// <typeparam name="TResponse">Response type of the request.</typeparam>
/// <param name="performanceMonitor">Performance monitor instance to track execution time.</param>
/// <param name="logger">Logger instance to log performance information and warnings.</param>
public class PerformanceBehavior<TRequest, TResponse>(
    IPerformanceMonitor performanceMonitor,
    ILogger<PerformanceBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    /// <summary>
    /// Handles the performance monitoring for a request. This method starts the performance monitor before executing the next handler in the pipeline and stops it afterward. It then checks the elapsed time and logs a warning if it exceeds a specified threshold (e.g., 500 ms). Otherwise, it logs an informational message with the execution time. This allows to identify potential performance issues in the application and optimize code execution as needed.
    /// </summary>
    /// <param name="request">Request to be monitored for performance.</param>
    /// <param name="next">Delegate that represents the next handler in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>The response from the next handler in the pipeline.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        performanceMonitor.Start();
        var response = await next();
        performanceMonitor.Stop();

        var elapsedMilliseconds = performanceMonitor.ElapsedMilliseconds;
        if (elapsedMilliseconds > 500) // Example threshold of 500 ms
        {
            logger.LogWarning("Request {RequestName} taking too long to run and exceeded threshold: {ElapsedMilliseconds} ms", requestName, elapsedMilliseconds);
        } 
        else       
        {       
            logger.LogInformation("Request {RequestName} completed in {ElapsedMilliseconds} ms", requestName, elapsedMilliseconds);
        }
        
        return response;
    }
}