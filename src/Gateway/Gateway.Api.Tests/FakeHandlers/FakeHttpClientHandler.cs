using System.Net;

namespace Gateway.Api.Tests.FakeHandlers
{
    public sealed class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handler;

        public FakeHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => _handler(request, cancellationToken);

        public static HttpResponseMessage Response(HttpStatusCode statusCode, string? content = null)
            => new(statusCode)
            {
                Content = content is null ? null : new StringContent(content)
            };
    }
}
``