using System.Data;
using ApnaDhobi.Infrastructure.Interfaces;
using ApnaDhobi.Infrastructure.Models;
using ApnaDhobi.Infrastructure.Enums;
using Microsoft.Data.SqlClient;

namespace ApnaDhobi.Infrastructure.Factories;

public sealed class DbConnectionFactory : IDbConnectionFactory
{
    public IDbConnection Create(DbSettings dbSettings)
    {
        return dbSettings.Provider switch
        {
            DbProviderType.SqlServer => CreateSqlConnection(dbSettings),
            _ => throw new NotSupportedException($"Database provider '{dbSettings.Provider}' is not supported."),
        };
    }

    /// <summary>
    /// Creates a SQL Server database connection using the provided settings.
    /// </summary>
    /// <param name="dbSettings">The database settings.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Connection string cannot be null or empty.</exception>
    private static IDbConnection CreateSqlConnection(DbSettings dbSettings)
    {
        if (dbSettings.ConnectionString == null)
        {
            throw new ArgumentException("Connection string cannot be null for SQL Server provider.");
        }

        var connectionBuilder = new SqlConnectionStringBuilder(dbSettings.ConnectionString)
        {
            ConnectTimeout = dbSettings.ConnectTimeoutMs / 1000,
            MaxPoolSize = dbSettings.MaxPoolSize,
            MinPoolSize = dbSettings.MinPoolSize,
            Pooling = true,
            ApplicationName = dbSettings.InstanceName,
            
        };

        return new SqlConnection(connectionBuilder.ConnectionString);
    }
}