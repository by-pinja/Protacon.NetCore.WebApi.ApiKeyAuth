using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    public static class ApiKey
    {
        public const string Scheme = "ApiKey";
        public static OpenApiSecurityScheme OpenApiSecurityScheme => new OpenApiSecurityScheme
        {
            Description = "Apikey authorization. Example: \"Authorization: ApiKey {key}\"",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Scheme = "ApiKey"
        };

        public static OpenApiSecurityRequirement OpenApiSecurityRequirement(string schemeId)
        {
            return new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = schemeId
                        }
                    }, new List<string>() }
            };
        }
    }
}