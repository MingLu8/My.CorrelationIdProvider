using System;
using Microsoft.AspNetCore.Http;

namespace My.CorrelationIdProvider
{
    public class CorrelationIdProvider : ICorrelationIdProvider
    {
        private IHttpContextAccessor HttpContextAccessor { get; }
        private IHeaderDictionary Headers => HttpContextAccessor.HttpContext.Request.Headers;
        public string CorrelationIdKey => "CorrelationId";

        public CorrelationIdProvider(IHttpContextAccessor httpContextAccessor)
        {
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
