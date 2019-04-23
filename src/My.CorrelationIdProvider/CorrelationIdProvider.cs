using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace My.CorrelationIdProvider
{
    public class CorrelationIdProvider : ICorrelationIdProvider
    {
        private IConfiguration Configuration { get; }
        private IHttpContextAccessor HttpContextAccessor { get; }
        private IHeaderDictionary Headers => HttpContextAccessor.HttpContext.Request.Headers;
        public string CorrelationIdKey => Configuration["CorrelationId"] ?? "X-Correlation-Id";

        public CorrelationIdProvider(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
        }

        public void AddCorrelationId()
        {
            if (Headers.TryGetValue(CorrelationIdKey, out var correlationId) &&
                (string) correlationId != null) return;

            Headers[CorrelationIdKey] = Guid.NewGuid().ToString("D");
        }

        public string GetCorrelationId()
        {
            return Headers[CorrelationIdKey];
        }
    }
}
