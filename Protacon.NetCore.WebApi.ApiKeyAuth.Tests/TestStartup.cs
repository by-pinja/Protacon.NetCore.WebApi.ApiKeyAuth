using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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
                    options.ValidApiKeys = new[] { "apiKeyForTests" };
                });

            services.AddSwaggerGen(c =>
            {
                var basePath = AppContext.BaseDirectory;

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "TestApi",
                        Version = "v1",
                        Description = ""
                    });

                c.AddSecurityDefinition("ApiKey", ApiKey.OpenApiSecurityScheme);
                c.AddSecurityRequirement(ApiKey.OpenApiSecurityRequirement("ApiKey"));
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Build.Proxy");
                c.RoutePrefix = "doc";
            });
            app.UseMvc();
        }

        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<TestStartup>()
                .Build()
                .Run();
        }
    }
}
