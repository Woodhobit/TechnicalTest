using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UserService.Api.Middleware;
using Xunit;

namespace UserService.UnitTest.MiddlewareTests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task WhenAnUnExpectedExceptionIsRaised_ErrorHandlingMiddlewareShouldHandleItToCustomErrorResponseAndCorrectHttpStatus()
        {
            // Arrange
            var middleware = new ErrorHandlingMiddleware((innerHttpContext) =>
            {
                throw new NullReferenceException("Test");
            },
            new NullLoggerFactory());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            //Act
            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var message = reader.ReadToEnd();

            //Assert
            message
            .Should()
            .BeEquivalentTo("Ops, something bad happened");

            context.Response.StatusCode
            .Should()
            .Be((int)HttpStatusCode.BadRequest);
        }
    }
}
