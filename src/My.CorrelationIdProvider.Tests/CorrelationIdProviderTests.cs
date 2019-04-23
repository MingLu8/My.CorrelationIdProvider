using System;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace My.CorrelationIdProvider.Tests
{
    public class CorrelationIdProviderTests
    {
        [Fact]
        public void GetCorrelationId_return_correlationId_from_request_header()
        {
            var correlationId = Guid.NewGuid().ToString("D");
            var configuration = Substitute.For<IConfiguration>();
            configuration["CorrelationId"].Returns("CorrelationIdKey");

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            httpContextAccessor.HttpContext.Returns(new DefaultHttpContext());
            httpContextAccessor.HttpContext.Request.Headers["CorrelationIdKey"] = correlationId;

            var correlationIdProvider = new CorrelationIdProvider(configuration, httpContextAccessor);
            correlationIdProvider.GetCorrelationId().Should().Be(correlationId);
        }

        [Fact]
        public void AddCorrelationId_with_correlationId_set_does_change_correlationId()
        {
            var correlationId = Guid.NewGuid().ToString("D");
            var configuration = Substitute.For<IConfiguration>();
            configuration["CorrelationId"].Returns("CorrelationIdKey");

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            httpContextAccessor.HttpContext.Returns(new DefaultHttpContext());
            httpContextAccessor.HttpContext.Request.Headers["CorrelationIdKey"] = correlationId;

            var correlationIdProvider = new CorrelationIdProvider(configuration, httpContextAccessor);
            correlationIdProvider.AddCorrelationId();

            httpContextAccessor.HttpContext.Request.Headers["CorrelationIdKey"].Should().BeEquivalentTo(correlationId);
        }

        [Fact]
        public void AddCorrelationId_without_existing_correlationId_set_correlationId()
        {
            var correlationId = Guid.NewGuid().ToString("D");
            var configuration = Substitute.For<IConfiguration>();
            configuration["CorrelationId"].Returns("CorrelationIdKey");

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            httpContextAccessor.HttpContext.Returns(new DefaultHttpContext());

            var correlationIdProvider = new CorrelationIdProvider(configuration, httpContextAccessor);
            correlationIdProvider.AddCorrelationId();

            httpContextAccessor.HttpContext.Request.Headers["CorrelationIdKey"].Should().NotBeNullOrEmpty();
        }
    }
}