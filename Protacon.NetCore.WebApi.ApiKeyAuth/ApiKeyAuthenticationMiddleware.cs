using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    internal class ApiKeyAuthenticationMiddleware : AuthenticationMiddleware<ApiKeyAuthenticationOptions>
    {
        public ApiKeyAuthenticationMiddleware(RequestDelegate next, IOptions<ApiKeyAuthenticationOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder)
            : base(next, options, loggerFactory, encoder)
        {
        }

        protected override AuthenticationHandler<ApiKeyAuthenticationOptions> CreateHandler()
        {
            return new ApiKeyAuthenticationHandler();
        }
    }
}
