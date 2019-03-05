using System.Collections.Generic;
using System.Net;
using Protacon.NetCore.WebApi.TestUtil;
using Xunit;

namespace Protacon.NetCore.WebApi.ApiKeyAuth.Tests
{
    public class ApiKeyTests
    {
        [Fact]
        public void WhenApiKeyIsNotGivenToAuthorizedApi_ThenReturnUnauthorized()
        {
            TestHost.Run<TestStartup>().Get("/v1/emptyget/")
                .ExpectStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void WhenCorrectApiKeyIsGiven_ThenReturnOk()
        {
            var headers = new Dictionary<string,string>
            {
                { "Authorization", "ApiKey apiKeyForTests" }
            };

            TestHost.Run<TestStartup>().Get("/v1/emptyget/", headers)
                .ExpectStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public void WhenApiKeyIsInvalid_ThenReturnUnauthorized()
        {
            var headers = new Dictionary<string,string>
            {
                { "Authorization", "ApiKey invalidApiKey" }
            };

            TestHost.Run<TestStartup>().Get("/v1/emptyget/", headers)
                .ExpectStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void WhenInvalidApiKeyIsGiven_ThenReturnUnauthorized()
        {
            var headers = new Dictionary<string,string>
            {
                { "Authorization", "ApiKey invalidApiKey" }
            };

            TestHost.Run<TestStartup>().Get("/v1/emptyget/", headers)
                .ExpectStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void WhenEmptyControllerMethodIsUsed_ThenSwaggerGeneratorWorkAsExpected()
        {
            TestHost.Run<TestStartup>().Get("/swagger/v1/swagger.json")
                .ExpectStatusCode(HttpStatusCode.OK);
        }
    }
}