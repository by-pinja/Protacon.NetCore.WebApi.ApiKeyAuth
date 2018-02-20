using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Protacon.NetCore.WebApi.ApiKeyAuth
{
    public class ApplyApiKeySecurityToDocument : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var apikeyRequired = filterPipeline
                .Select(f => f.Filter)
                .Where(x => x is AuthorizeFilter)
                .Cast<AuthorizeFilter>()
                .Any(x => x.Policy.AuthenticationSchemes.Any(scheme => scheme.ToLower() == "apikey"));

            if (apikeyRequired)
            {
                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<IParameter>();
                }

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "ApiKey is designed to use with integrations, for example: 'ApiKey apikeyfortesting'",
                    Type = "string",
                    Default = "ApiKey apikeyfortesting"
                });
            }
        }
    }
}
