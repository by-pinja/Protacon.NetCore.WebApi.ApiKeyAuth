[![Build status](https://ci.appveyor.com/api/projects/status/3upb01m4msrjt65e?svg=true)](https://ci.appveyor.com/project/savpek/protacon-netcore-webapi-apikeyauth)

[![Nuget](https://img.shields.io/nuget/dt/Protacon.NetCore.WebApi.ApiKeyAuth.svg)](https://www.nuget.org/packages/Protacon.NetCore.WebApi.ApiKeyAuth/)

# Simple configurable middleware for ApiKey authentication

## Configuring on startup
```cs
    services.Configure<ApiKeyAuthenticationOptions>(x => x.ValidApiKeys = new List<string>() { "yourapiKeyhere" });
```

## Adding authentication
```cs
    // Add service
    services
        .AddAuthentication()
        .AddApiKeyAuth(options =>
        {
            options.ValidApiKeys = new List<string>{"thiIsOneNotSoSecureApiKey"};
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

# Support for swagger documentation
Package adds support for webapi fields on swagger documentation.
```cs
    services.AddSwaggerGen(c =>
    {
        c.OperationFilter<ApplyApiKeySecurityToDocument>();
    });
```

See Swagger UI for further examples.