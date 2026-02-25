using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Gateway.Api.HealthChecks
{
    /// <summary>
    /// Health check for downstream services.
    /// </summary>
    public class DownstreamHealthCheck : IHealthCheck
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DownstreamHealthCheck(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        /// <summary>
        /// Checks the health of downstream services.
        /// </summary>
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var downstreamServices = _configuration.GetSection("DownstreamServices").Get<List<string>>();

                if (downstreamServices == null || downstreamServices.Count == 0)
                {
                    return HealthCheckResult.Unhealthy("No downstream services configured");
                }

                var failedServices = new List<string>();

                foreach (var service in downstreamServices)
                {
                    try
                    {
                        var response = await _httpClient.GetAsync($"{service}/health", cancellationToken);
                        if (!response.IsSuccessStatusCode)
                        {
                            failedServices.Add($"{service} (Status: {response.StatusCode})");
                        }
                    }
                    catch (Exception ex)
                    {
                        failedServices.Add($"{service} (Error: {ex.Message})");
                    }
                }

                if (failedServices.Count > 0)
                {
                    return HealthCheckResult.Degraded(
                        $"Some downstream services are unhealthy: {string.Join(", ", failedServices)}");
                }

                return HealthCheckResult.Healthy("All downstream services are healthy");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Health check failed: {ex.Message}");
            }
        }
    }
}