namespace ApnaDhobi.Infrastructure.Interfaces;

/// <summary>
/// Defines a contract for executing commands and queries in a CQRS pattern. This interface abstracts away the details of how commands and queries are handled, allowing for flexibility in implementation (e.g., using MediatR, custom handlers, etc.). The methods return a Result or Result<T> to encapsulate the success status, error messages, and any data returned from the operation.
/// </summary>
public interface IRunner
{
    /// <summary>
    /// Executes a command that does not return a payload. The result indicates whether the operation was successful and contains any error messages if it failed.
    /// </summary>
      /// <param name="command">Command to be executed.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>It will return a <see cref="Result"/> indicating success or failure of the command execution.</returns>
    Task RunAsync(ICommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a command that returns a payload of type T wrapped in a Result. The Result indicates whether the operation was successful, contains any error messages if it failed, and includes the data payload if it succeeded.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="command">Command to be executed.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>It will return a T indicating success or failure of the command execution.</returns>
    Task<T> RunAsync<T>(ICommand<T> command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a query that returns a payload of type T wrapped in a Result. The Result indicates whether the operation was successful, contains any error messages if it failed, and includes the data payload if it succeeded.
    /// </summary>
    /// <param name="query">Query to be executed.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    Task RunAsync(IQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a query that returns a payload of type T wrapped in a Result. The Result indicates whether the operation was successful, contains any error messages if it failed, and includes the data payload if it succeeded.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">Query to be executed.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>It will return a T indicating success or failure of the query execution.</returns>
    Task<T> RunAsync<T>(IQuery<T> query, CancellationToken cancellationToken = default);
}