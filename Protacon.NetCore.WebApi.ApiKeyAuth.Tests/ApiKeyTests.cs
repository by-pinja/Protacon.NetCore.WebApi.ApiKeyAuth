using System.Net;
using Protacon.NetCore.WebApi.TestUtil;
using Xunit;

namespace Protacon.NetCore.WebApi.ApiKeyAuth.Tests
{

    public class ApiKeyTests
    {
        [Fact]
        public void WhenEmptyControllerMethodIsUsed_ThenSwaggerGeneratorWorkAsExpected()
        {
            TestHost.Run<TestStartup>().Get("/swagger/v1/swagger.json")
                .ExpectStatusCode(HttpStatusCode.OK);
        }
    }
}