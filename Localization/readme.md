# Localization

## RequestUICulture
**RequestUICulture** represent the most widely used cultural features.

### How to setup?

#### Step 1
Specify the supported cultures
```
public class Startup
{
  ...

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddControllersWithViews();

    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ja-JP"),
        new CultureInfo("fa-IR"),
    };
  }
}
```

#### Step 2
Configure request localization options
```
public class Startup
{
  ...

  public void ConfigureServices(IServiceCollection services)
  {
    ...

    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ja-JP"),
        new CultureInfo("fa-IR"),
    };

    services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new RequestCulture("en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });
  }
}
```
You can change which providers are configured to determine the culture for requests, or even add a custom provider with your own logic. The providers will be asked in order to provide a culture for each request, and the first to provide a non-null result that is in the configured supported cultures list will be used. By default, the following built-in providers are configured:
- QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
- CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
- AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
```
  ...

  services.Configure<RequestLocalizationOptions>(options =>
  {
      options.DefaultRequestCulture = new RequestCulture("en-US");
      options.SupportedCultures = supportedCultures;
      options.SupportedUICultures = supportedCultures;

      options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
  });
```

#### Step 3
Add a transient service of IRequestUICulture.
```
  ...

  services.AddTransient<IRequestUICulture, RequestUICulture>();
```
**Note:** Sometimes you need to add IHttpContextAccessor service, because IHttpContextAccessor is no longer wired up by default, so you have to register it yourself.
```
  ...

  services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
      .AddTransient<IRequestUICulture, RequestUICulture>();
```


## MultiResourceLocalizer
**MultiResourceLocalizer** is a service to enable localization for applications with multiple resource sources.

##### code snippet:
```
public class Startup
{
  ...

  public void ConfigureServices(IServiceCollection services)
  {
    ...

    services.AddMvc()
        .AddViewLocalization(options => options.ResourcesPath = "")
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
            {
                return new MultiResourceLocalizer(factory, typeof(ModelsResource), type);
            };
        });
  }
}
```
