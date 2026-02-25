namespace ApnaDhobi.Infrastructure.Interfaces;

/// <summary>
/// Interface for monitoring performance of operations, allowing to track execution time and identify potential bottlenecks.
/// </summary>
public interface IPerformanceMonitor
{
    /// <summary>
    /// Gets the elapsed time in milliseconds for the monitored operation. This property should return the total time taken for the operation being monitored, allowing to analyze performance and identify areas for optimization.
    /// </summary>
    long ElapsedMilliseconds { get; }

    /// <summary>
    /// Starts tracking performance for an operation. This method should be called at the start of the operation to initialize any necessary resources or timers.
    /// </summary>
    void Start();

    /// <summary>
    /// Stops tracking performance for an operation. This method should be called at the end of the operation to finalize the tracking and calculate the elapsed time. The implementation can log or store the performance data as needed for analysis and optimization.
    /// </summary>
    void Stop();
}