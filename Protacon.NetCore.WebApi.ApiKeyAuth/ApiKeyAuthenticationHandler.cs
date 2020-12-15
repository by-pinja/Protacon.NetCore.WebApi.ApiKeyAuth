using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    internal class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly ILogger<ApiKeyAuthenticationHandler> _logger;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock: clock)
        {
            _logger = logger.CreateLogger<ApiKeyAuthenticationHandler>();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.TryGetValue("Authorization", out StringValues headerValue))
                return Task.FromResult(
                    AuthenticateResult.Fail(
                        "Missing or malformed 'Authorization' header, expected 'Token token_value'"));

            var apiKey = headerValue.FirstOrDefault(x => x.StartsWith("ApiKey"));

            if (apiKey == null || !apiKey.Any())
                return Task.FromResult(AuthenticateResult.Fail("Invalid key format, expected 'ApiKey key'"));

            var parsedKey = Regex.Replace(apiKey, "^ApiKey", "").Trim();
            var match = Options.ValidApiKeys.SingleOrDefault(x => x == parsedKey);

            _logger.LogDebug($"Trying to match apikey '{parsedKey}' against keys '{string.Join(",", Options.ValidApiKeys)}'");

            if (match == null)
                return Task.FromResult(AuthenticateResult.Fail("Invalid ApiKey"));

            var identity = new ClaimsIdentity("apikey");
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), null, "apikey");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}