using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Protacon.NetCore.WebApi.ApiKeyAuth.Tests
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class TestController: Controller
    {
        [HttpGet("/v1/emptyget/")]
        [ProducesResponseType(typeof(object[]), 200)]
        public IActionResult EmptyGet()
        {
            return Ok(new object[] {});
        }
    }
}