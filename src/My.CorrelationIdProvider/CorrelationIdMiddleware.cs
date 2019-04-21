using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace My.CorrelationIdProvider
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICorrelationIdProvider correlationIdProvider)
        {
            correlationIdProvider.AddCorrelationId();
            await _next(context);
        }
    }
}