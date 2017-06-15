[![Build status](https://ci.appveyor.com/api/projects/status/cv4tmosjkxe7dpom/branch/master?svg=true)](https://ci.appveyor.com/project/savpek/protacon-netcore-webapi-testutil/branch/master)

[![Nuget](https://img.shields.io/nuget/dt/Protacon.NetCore.WebApi.ApiKeyAuth.svg)](https://www.nuget.org/packages/Protacon.NetCore.WebApi.ApiKeyAuth/)

# Simple configurable middleware for ApiKey authentication

## Configuring on startup
```cs
    app.Configure
```

## Configuring with congiruation json.
```cs
    services.Configure<ApiKeyAuthenticationOptions>(Configuration.GetSection("ApiAuthentication"));
```

## Adding middleware to application.
```cs
    app.UseMiddleware<ApiKeyAuthenticationMiddleware>();
```

## Using on authorization
```cs
    [Authorize(ActiveAuthenticationSchemes = "ApiKey")]
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