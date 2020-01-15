# Localization

## RequestUICulture
**RequestUICulture** represent the most widely used cultural features.

Property | Type | Description
-------- | ---- | -----------
Culture | CultureInfo | Current UI CultureInfo.
IsRTL | Boolean | Gets a value indicating whether the current TextInfo object represents a writing system where text flows from right to left.
Direction | String | Gets a value indicating whether the current culture direction.
Language | String | Gets the ISO 639-1 two-letter code for the language of the current CultureInfo.
Name | String | Gets the culture name in the format languagecode2-country/regioncode2.


### How to setup localization?

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

**Note:** You need to add IHttpContextAccessor service too, because IHttpContextAccessor is no longer wired up by default, so you have to register it yourself.
```
  ...

  services.AddHttpContextAccessor()
      .AddTransient<IRequestUICulture, RequestUICulture>();
```

#### Step 4
Configure the HTTP request pipeline
```
public class Startup
{
  ...

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(locOptions.Value);
    ...
  }
}
```

### How to use?

#### Step 1
Inject IRequestUICulture to _ViewImports or _Layout
```
  @inject IRequestUICulture Culture
```

#### Step 2
You can use the injected culture to localize the UI as follows:
```
<!DOCTYPE html>
<html lang="@Culture.Language" dir="@(Culture.Direction)">
...
<body class="@Culture.Name">
...
```
Simple css class of body element for specific cultures:
```
.en-US {
    font-family: Verdana, Geneva, Tahoma, sans-serif;
    text-align: left;
}

.ja-JP {
    font-family: "ヒラギノ角ゴ Pro W3", "Hiragino Kaku Gothic Pro",Osaka, "メイリオ", Meiryo, "ＭＳ Ｐゴシック", "MS PGothic", sans-serif;
    text-align: left;
}

.fa-IR {
    font-family: Verdana, Geneva, Tahoma, sans-serif;
    text-align: right;
}
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
