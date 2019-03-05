using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Protacon.NetCore.WebApi.ApiKeyAuth.Tests
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication()
                .AddApiKeyAuth(options =>
                {
                    options.ValidApiKeys = new[] {"apiKeyForTests"};
                });

            services.AddSwaggerGen(c =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "TestApi",
                        Version = "v1",
                        Description = ""
                    });
                c.OperationFilter<ApplyApiKeySecurityToDocument>();
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseSwagger();
            app.UseMvc();
        }

        public static void Main(string[] args) {}
    }
}
