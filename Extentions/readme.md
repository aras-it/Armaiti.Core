# Extentions

### ColorExtensions
ColorExtensions provide functionality to convert color formats such as Hex to Rgb and vice versa.

##### code snippet:
```
System.Drawing.Color red = "rgb(255, 0, 0)".ParseColor();
System.Drawing.Color blue = "#0000FF".ParseColor();

string cssColor = red.ToHtml(); // returns #FF0000
```


### HttpContextExtensions
HttpContextExtensions provides the ability to retrieve client IP address.

##### code snippet:
```
public class CommentController
{
  ...
  [HttpPost]
  public async Task<IActionResult> Save(CommentViewModel model)
  {
    ...
    var userIP = HttpContext.GetRemoteIP();
    ...
  }
}
```
**Note:** userIP is an instance of [IP class](https://github.com/aras-it/Armaiti.Core/Http/IP.cs) include [IPAddress](https://docs.microsoft.com/en-us/dotnet/api/system.net.ipaddress?view=netcore-3.1) and [IPHostEntry](https://docs.microsoft.com/en-us/dotnet/api/system.net.iphostentry?view=netcore-3.1).

**Note:** This method returns :::1 (127.0.0.1) on the development machine but returns the correct value on the web server.


### ModelBindingErrorLocalizerExtensions
ModelBindingErrorLocalizerExtensions sets all properties of the [ModelBindingMessageProvider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinding.metadata.modelbindingmessageprovider?view=aspnetcore-3.1) to the specified culture of localizer.
This method is used internally by **AddModelBindingMessagesLocalization** and **AddModelLocalization** in **MvcOptionsLocalizerExtensions**.

### MvcOptionsLocalizerExtensions
MvcOptionsLocalizerExtensions localize MVC [ModelBinding](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1) messages and [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1) attributes.

##### code snippet:
```
public class Startup
{
  ...
  public void ConfigureServices(IServiceCollection services)
  {
    ...
    services.AddMvc()
        .AddModelBindingMessagesLocalization<ModelBindingsResource>()
        .AddDataAnnotationsLocalization<DataAnnotationsResource>();

    // or add model localization for both above extension methods in one step

    services.AddMvc()
        .AddModelLocalization<ModelsResource>();
    ...
  }
}
```
**Note:** All resx files are available [here](https://github.com/aras-it/Armaiti.Core/Files/Resources)


### PreventInjectionExtensions
PreventInjectionExtensions provide functionality to check whether the sql string is safe to run on the sql-server.

##### code snippet:
```
public class HomeController
{
  ...
  [HttpPost]
  [AllowAnonymous]
  public async Task<IActionResult> Search(string searchCondition)
  {
    if (!searchCondition.IsSqlSafe())
      return Forbid();
     
    ...
  }
}
```