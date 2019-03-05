[![Build status](https://ci.appveyor.com/api/projects/status/3upb01m4msrjt65e?svg=true)](https://ci.appveyor.com/project/savpek/protacon-netcore-webapi-apikeyauth)

[![Nuget](https://img.shields.io/nuget/dt/Protacon.NetCore.WebApi.ApiKeyAuth.svg)](https://www.nuget.org/packages/Protacon.NetCore.WebApi.ApiKeyAuth/)

# Simple configurable middleware for ApiKey authentication

## Adding authentication

```cs
    // Add service
    services
        .AddAuthentication()
        .AddApiKeyAuth(options =>
        {
            if(!Configuration.GetChildren().Any(x => x.Key == "ApiAuthentication"))
                throw new InvalidOperationException($"Expected 'ApiAuthentication' section.");

            var keys = Configuration.GetSection("ApiAuthentication:Keys")
                .AsEnumerable()
                .Where(x => x.Value != null)
                .Select(x => x.Value);

            options.ValidApiKeys = keys;
        });

    // Configuration (this comes from net core 2.x+, not from this library.)
    app.UseAuthentication();
```

## Using on authorization

```cs
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class ExampleController : Controller
    {
    }
```

```cs
    public class ExampleController : Controller
    {
        [Authorize(AuthenticationSchemes = "ApiKey")]
        [HttpPost("...")]
        public IActionResult Method() {}
    }
```

# Support for swagger documentation

Package adds support for webapi fields on swagger documentation.

```cs
    services.AddSwaggerGen(c =>
    {
        c.OperationFilter<ApplyApiKeySecurityToDocument>();
    });
```

See Swagger UI for further examples.