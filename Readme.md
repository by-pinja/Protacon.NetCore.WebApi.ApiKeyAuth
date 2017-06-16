[![Build status](https://ci.appveyor.com/api/projects/status/3upb01m4msrjt65e?svg=true)](https://ci.appveyor.com/project/savpek/protacon-netcore-webapi-apikeyauth)

[![Nuget](https://img.shields.io/nuget/dt/Protacon.NetCore.WebApi.ApiKeyAuth.svg)](https://www.nuget.org/packages/Protacon.NetCore.WebApi.ApiKeyAuth/)

# Simple configurable middleware for ApiKey authentication

## Configuring on startup
```cs
    services.Configure<ApiKeyAuthenticationOptions>(x => x.Keys = new List<string>() {});
```

## Configuring with congiruation json.
```cs
    services.Configure<ApiKeyAuthenticationOptions>(Configuration.GetSection("ApiAuthentication"));
```

And configuration section

```json
{
  "ApiAuthentication": {
    "Keys": ["somegoodkeyishere"]
  }
}
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