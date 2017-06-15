using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    internal class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.TryGetValue("Authorization", out StringValues headerValue))
                return Task.FromResult(
                    AuthenticateResult.Fail(
                        "Missing or malformed 'Authorization' header, expected 'Token token_value'"));

            var apiKey = headerValue.FirstOrDefault(x => x.StartsWith("ApiKey"));

            if (apiKey == null || !apiKey.Any())
                return Task.FromResult(AuthenticateResult.Fail("Invalid key format, expected 'ApiKey key'"));

            var match = Options.Keys.SingleOrDefault(x => x == apiKey.Replace("ApiKey", "").Trim());

            if (match == null)
                return Task.FromResult(AuthenticateResult.Fail("Invalid ApiKey"));

            var identity = new ClaimsIdentity("apikey");
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), null, "apikey");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}