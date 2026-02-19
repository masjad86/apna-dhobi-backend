namespace Gateway.Api.Models.Settings
{
    public class RateLimiterSetting
    {
        public int RequestsPerMinute { get; set; }
        public int RequestsPerHour { get; set; }
        public int BurstSize { get; set; }
        public bool Enabled { get; set; }
        public List<string> ExcludedPaths { get; set; } = new();
        public Dictionary<string, int> EndpointLimits { get; set; } = new();
    }
}