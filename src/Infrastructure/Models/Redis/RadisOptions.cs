namespace ApnaDhobi.Infrastructure.Models;
public sealed class RedisOptions
{
    public string Configuration { get; set; } = "";
    public string InstanceName { get; set; } = "";
    public int ConnectTimeoutMs { get; set; } = 5000;
    public int SyncTimeoutMs { get; set; } = 5000;
    public bool AbortOnConnectFail { get; set; } = false;
    public int ConnectRetry { get; set; } = 3;
}
