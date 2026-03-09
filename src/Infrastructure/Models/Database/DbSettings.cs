using ApnaDhobi.Infrastructure.Enums;
namespace ApnaDhobi.Infrastructure.Models;

public class DbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public DbProviderType Provider { get; set; } = DbProviderType.SqlServer;
    public int CommandTimeoutSeconds { get; set; } = 30;
    public int MaxPoolSize { get; set; } = 100;
    public int MinPoolSize { get; set; } = 0;
    public string InstanceName { get; set; } = string.Empty;
    public bool AbortOnConnectFail { get; set; } = false;
    public int ConnectTimeoutMs { get; set; } = 5000;
    public int SyncTimeoutMs { get; set; } = 1000;
}