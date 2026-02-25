public class CacheConnectionOptions
{
    /// <summary>
    /// The connection string for the cache. This is typically used for distributed caches like Redis or Memcached to specify how to connect to the cache server. The format of the connection string will depend on the specific cache implementation being used.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// The database number to use for the cache. This is specific to certain cache implementations (e.g., Redis) that support multiple databases. Default is 0.
    /// </summary>
    public int Database { get; set; } = 0;

    /// <summary>
    /// The timeout for cache operations. This specifies how long to wait for a response from the cache server before timing out. Default is 5 seconds.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Additional configuration options specific to the cache implementation being used. This can include settings such as SSL options, connection pool settings, or other parameters that may be required to properly configure the cache connection. 
    /// This property is optional and can be used to provide any additional settings that are not covered by the
    /// </summary>
    public Dictionary<string, string>? AdditionalOptions { get; set; }
}