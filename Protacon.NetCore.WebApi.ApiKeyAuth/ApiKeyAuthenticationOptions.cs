using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    public class ApiKeyAuthenticationOptions: AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "ApiKey";
        public string Scheme => DefaultScheme;
        public IEnumerable<string> ValidApiKeys { get; set; } = new List<string>();
    }
}
