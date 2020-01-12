using Armaiti.Core.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace Armaiti.Core
{
    /// <summary>
    /// Extension methods for configuring MVC model binding and data annotations localization.
    /// </summary>
    public static class MvcOptionsLocalizationExtensions
    {
        /// <summary>
        /// Adds both MVC model binding and data annotations localization to the application.
        /// </summary>
        /// <typeparam name="TResource">Type of resource source.</typeparam>
        /// <param name="mvc">The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</returns>
        public static IMvcBuilder AddModelLocalization<TResource>(this IMvcBuilder mvc) where TResource : class
        {
            return mvc
                .AddModelBindingMessagesLocalization<TResource>()
                .AddDataAnnotationsLocalization<TResource>();
        }

        /// <summary>
        /// Adds both MVC model binding and data annotations localization to the application.
        /// </summary>
        /// <param name="mvc">The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</param>
        /// <param name="resourceType">Type of resource source.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</returns>
        public static IMvcBuilder AddModelLocalization(this IMvcBuilder mvc, Type resourceType)
        {
            return mvc
                .AddModelBindingMessagesLocalization(resourceType)
                .AddDataAnnotationsLocalization(resourceType);
        }

        /// <summary>
        /// Adds MVC data annotations localization to the application.
        /// </summary>
        /// <typeparam name="TResource">Type of resource source.</typeparam>
        /// <param name="mvc">The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</returns>
        public static IMvcBuilder AddDataAnnotationsLocalization<TResource>(this IMvcBuilder mvc) where TResource : class
        {
            return mvc.AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return new MultiResourceLocalizer(factory, typeof(TResource), type);
                });
        }

        /// <summary>
        /// Adds MVC data annotations localization to the application.
        /// </summary>
        /// <param name="mvc">The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</param>
        /// <param name="resourceType">Type of resource source.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</returns>
        public static IMvcBuilder AddDataAnnotationsLocalization(this IMvcBuilder mvc, Type resourceType)
        {
            return mvc.AddDataAnnotationsLocalization(options =>
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return new MultiResourceLocalizer(factory, resourceType, type);
                });
        }

        /// <summary>
        /// Adds MVC model binding localization to the application.
        /// </summary>
        /// <typeparam name="TResource">Type of resource source.</typeparam>
        /// <param name="mvc">The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</returns>
        public static IMvcBuilder AddModelBindingMessagesLocalization<TResource>(this IMvcBuilder mvc) where TResource : class
        {
            return mvc.AddMvcOptions(options =>
            {
                var resourceType = typeof(TResource);
                var assemblyName = new AssemblyName(resourceType.GetTypeInfo().Assembly.FullName);
                var factory = mvc.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var localizer = factory.Create(resourceType.Name, assemblyName.Name);

                options.ModelBindingMessageProvider.SetLocalizedModelBindingErrorMessages(localizer);
            });
        }

        /// <summary>
        /// Adds MVC model binding localization to the application.
        /// </summary>
        /// <param name="mvc">The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</param>
        /// <param name="resourceType">Type of resource source.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IMvcBuilder.</returns>
        public static IMvcBuilder AddModelBindingMessagesLocalization(this IMvcBuilder mvc, Type resourceType)
        {
            return mvc.AddMvcOptions(options =>
            {
                var factory = mvc.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var localizer = factory.Create(resourceType);

                options.ModelBindingMessageProvider.SetLocalizedModelBindingErrorMessages(localizer);
            });
        }
    }
}
