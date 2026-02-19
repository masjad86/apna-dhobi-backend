using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Gateway.Api.HealthChecks
{
    /// <summary>
    /// Health check for the Gateway API.
    /// </summary>
    public class GatewayHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Checks the health of the Gateway API.
        /// </summary>
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.FromResult(
                    HealthCheckResult.Healthy("Gateway is healthy"));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    HealthCheckResult.Unhealthy(
                        "Gateway health check failed",
                        ex));
            }
        }
    }
}