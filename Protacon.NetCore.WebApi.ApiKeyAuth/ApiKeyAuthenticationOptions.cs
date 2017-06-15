using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    public class ApiKeyAuthenticationOptions: AuthenticationOptions
    {
        public ApiKeyAuthenticationOptions()
        {
            AuthenticationScheme = "ApiKey";
        }

        public List<string> Keys { get; set; }
    }
}
