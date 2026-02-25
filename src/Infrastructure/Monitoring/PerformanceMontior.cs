using System.Diagnostics;
using ApnaDhobi.Infrastructure.Interfaces;

namespace ApnaDhobi.Infrastructure.Monitoring;

/// <summary>
/// Implementation of IPerformanceMonitor that uses Stopwatch to track execution time of operations. This class provides methods to start and stop the performance monitoring, allowing to measure the elapsed time for any given operation. It can be used in various parts of the application to identify performance bottlenecks and optimize code execution.
/// </summary>
public sealed class PerformanceMonitor(Stopwatch stopwatch) : IPerformanceMonitor
{
    private Stopwatch _stopwatch = stopwatch;

    public long ElapsedMilliseconds => _stopwatch?.ElapsedMilliseconds ?? 0;

    /// <inheritdoc>
    public void Start()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    /// <inheritdoc>
    public void Stop()
    {
        _stopwatch?.Stop();
    }
}