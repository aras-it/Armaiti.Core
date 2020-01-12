# Armaiti.Core

## What is Armaiti.Core?

Armaiti.Core is a nugget package for asp.net core appliations include general
utilities and tools such as localization, email service, prevent SQL injection
and more.

## Features

-   **Email Service** send SMTP email with/without message template. ([more…](https://github.com/aras-it/Armaiti.Core/Services#EmailService))

-   **Localization** ([more…](https://github.com/aras-it/Armaiti.Core/Localization#Localization))

    -   **MultiResourceLocalizer** is a service to enable localization for
        applications with multiple resource sources.

    -   **RequestUICulture** represent the most widely used cultural features.

-   **LanguagesTagHelper** creates a bootstrap dropdown menu for available
    languages in supported cultures of [RequestLocalizationOptions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.requestlocalizationoptions?view=aspnetcore-3.1). ([more…](https://github.com/aras-it/Armaiti.Core/TagHelpers/readme.md))

-   **Extentions** ([more…](https://github.com/aras-it/Armaiti.Core/Extentions#Extentions))

    -   **ColorExtensions** provide functionality to convert color formats
        such as Hex to Rgb and vice versa.

    -   **HttpContextExtensions** provide functionality to get client remote IP
        address.

    -   **ModelBindingErrorLocalizerExtensions** sets all properties of the [ModelBindingMessageProvider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinding.metadata.modelbindingmessageprovider?view=aspnetcore-3.1) to the specified culture of localizer.

    -   **MvcOptionsLocalizerExtensions** localize MVC [ModelBinding](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1) messages and [DataAnnotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1) attributes.

    -   **PreventInjectionExtensions** provide functionality to check whether
        the sql string is safe to run on the sql-server db engine.


## Installation [![nuget](https://cdn.arasit.com/img/nuget/nuget1.1.0.svg)](https://www.nuget.org/packages/Armaiti.Core/)

**Package Manager:**  `PM> Install-Package Armaiti.Core -Version 1.1.0`

**.NET CLI:**         `> dotnet add package Armaiti.Core --version 1.1.0`

**PackageReference:** `<PackageReference Include="Armaiti.Core" Version="1.1.0" />`

## Dependencies

.NETCoreApp 3.1

## License
MIT
