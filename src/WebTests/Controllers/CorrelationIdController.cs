using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My.CorrelationIdProvider;

namespace WebTests.Controllers
{
    [Route("")]
    [ApiController]
    public class CorrelationIdController : ControllerBase
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public CorrelationIdController(ICorrelationIdProvider correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        [HttpGet("/getCorrelationIdKey")]
        public ActionResult<string> CorrelationIdKey()
        {
            return $"correlationIdKey:{_correlationIdProvider.CorrelationIdKey}";
        }

        // GET api/values
        [HttpGet("/withCorrelationIdHeader")]
        public ActionResult<string> WithCorrelationId([FromHeader(Name = "X-Correlation-Id")] string correlationId)
        {
            return $"correlationId:{_correlationIdProvider.GetCorrelationId()}";
        }

        [HttpGet("/withoutCorrelationIdHeader")]
        public ActionResult<string> WithoutCorrelationId()
        {
            return $"correlationId:{_correlationIdProvider.GetCorrelationId()}";
        }
    }
}
