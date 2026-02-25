namespace Gateway.Api.Tests.Settings;

public sealed class GatewayHealthCheckTests
    {
        [Fact]
        public async Task CheckHealthAsync_AllDownstreamServicesHealthy_ReturnsHealthy()
        {
            // Arrange
            var downstreamServices = new List<string> { "http://service1", "http://service2" };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["DownstreamServices:0"] = downstreamServices[0],
                    ["DownstreamServices:1"] = downstreamServices[1]
                })
                .Build();

            var httpClient = new HttpClient(new FakeHttpMessageHandler((request, cancellationToken) =>
            {
                return Task.FromResult(FakeHttpMessageHandler.Response(HttpStatusCode.OK));
            }));

            var healthCheck = new DownstreamHealthCheck(httpClient, configuration);

            // Act
            var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());

            // Assert
            Assert.Equal(HealthStatus.Healthy, result.Status);
            Assert.Equal("All downstream services are healthy", result.Description);
        }

        [Fact]
        public async Task CheckHealthAsync_SomeDownstreamServicesUnhealthy_ReturnsDegraded()
        {
            // Arrange
            var downstreamServices = new List<string> { "http://service1", "http://service2" };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["DownstreamServices:0"] = downstreamServices[0],
                    ["DownstreamServices:1"] = downstreamServices[1]
                })
                .Build();

            var httpClient = new HttpClient(new FakeHttpMessageHandler((request, cancellationToken) =>
            {
                if (request.RequestUri!.ToString().Contains("service1"))
                {
                    return Task.FromResult(FakeHttpMessageHandler.Response(HttpStatusCode.OK));
                }
                else
                {
                    return Task.FromResult(FakeHttpMessageHandler.Response(HttpStatusCode.InternalServerError));
                }
            }));

            var healthCheck = new DownstreamHealthCheck(httpClient, configuration);

            // Act
            var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());

            // Assert
            Assert.Equal(HealthStatus.Degraded, result.Status);
            Assert.Contains("Some downstream services are unhealthy", result.Description);
        }

        [Fact]
        public async Task CheckHealthAsync_NoDownstreamServicesConfigured_ReturnsUnhealthy()
        {