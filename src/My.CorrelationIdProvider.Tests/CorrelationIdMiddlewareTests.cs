using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace My.CorrelationIdProvider.Tests
{
    public class CorrelationIdProviderMiddlewareTests
    {
        [Fact]
        public async void AddCorrelationId_is_call_once()
        {
            var correlationIdProvider = Substitute.For<ICorrelationIdProvider>();

            var middleware = new CorrelationIdMiddleware(next: async (innerHttpContext) => { });

            await middleware.Invoke(new DefaultHttpContext(), correlationIdProvider);
            correlationIdProvider.Received(1).AddCorrelationId();
        }
    }
}
