# LanguagesTagHelper
LanguagesTagHelper creates a bootstrap dropdown menu for available languages in supported cultures of [RequestLocalizationOptions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.requestlocalizationoptions?view=aspnetcore-3.1).

### How to use?

#### Step 1
Import Armaiti.Core TagHelper into _ViewImports

```
@addTagHelper *, Armaiti.Core
```

#### Step 2
Add the 'languages' ​​tag where you want to display the language menu. The navbar in _Layout is a good place to do this.

```
<languages />
```

#### Step 3
To localize the menu caption you can inject IViewLocalizer or one of the localizer services into _Layout or _ViewImports

```
@addTagHelper *, Armaiti.Core
@inject IStringLocalizer<SharedResource> Localizer;
```

and set the caption attribute

```
<languages caption="@Localizer["Languages"]" />
```

You can also change the css class of the language menu. The default class is "btn-light".

```
<languages caption="@Localizer["Languages"]" class="btn-primary" />
```
