using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    public static class ApiKeyAuthenticationExtensions
    {
        public static AuthenticationBuilder AddApiKeyAuth(this AuthenticationBuilder builder, Action<ApiKeyAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, configureOptions);
        }

        public static AuthenticationBuilder AddApiKeyAuth(this AuthenticationBuilder builder, params string[] validApiKeys)
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, options => options.ValidApiKeys = validApiKeys);
        }

        public static AuthenticationBuilder AddDisabledApiKeyAuth(this AuthenticationBuilder builder)
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, DisabledApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, _ => {});
        }
    }
}
