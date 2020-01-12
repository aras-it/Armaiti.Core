using Armaiti.Core.Localization.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;

namespace Armaiti.Core
{
    /// <summary>
    /// Extension methods for configuring MVC model binding error messages localization.
    /// </summary>
    public static class ModelBindingErrorLocalizerExtensions
    {
        /// <summary>
        /// Sets all properties of the <see cref="ModelBindingMessageProvider"/> to the specified culture of <paramref name="localizer"/>.
        /// </summary>
        /// <param name="provider">An instance of the <see cref="DefaultModelBindingMessageProvider"/>.</param>
        /// <param name="localizer">The service that provides localized strings.</param>
        public static void SetLocalizedModelBindingErrorMessages(this DefaultModelBindingMessageProvider provider, IStringLocalizer localizer)
        {
            provider.SetNonPropertyValueMustBeANumberAccessor(()
                => localizer[ModelBindingResources.HtmlGeneration_NonPropertyValueMustBeNumber]);

            provider.SetValueIsInvalidAccessor((x)
                => localizer[ModelBindingResources.HtmlGeneration_ValueIsInvalid, x]);

            provider.SetValueMustBeANumberAccessor((x)
                => localizer[ModelBindingResources.HtmlGeneration_ValueMustBeNumber, x]);

            provider.SetMissingKeyOrValueAccessor(()
                => localizer[ModelBindingResources.KeyValuePair_BothKeyAndValueMustBePresent]);

            provider.SetMissingBindRequiredValueAccessor((x)
                => localizer[ModelBindingResources.ModelBinding_MissingBindRequiredMember, x]);

            provider.SetMissingRequestBodyRequiredValueAccessor(()
                => localizer[ModelBindingResources.ModelBinding_MissingRequestBodyRequiredMember]);

            provider.SetValueMustNotBeNullAccessor((x)
                => localizer[ModelBindingResources.ModelBinding_NullValueNotValid, x]);

            provider.SetAttemptedValueIsInvalidAccessor((x, y)
                => localizer[ModelBindingResources.ModelState_AttemptedValueIsInvalid, x, y]);

            provider.SetNonPropertyAttemptedValueIsInvalidAccessor((x)
                => localizer[ModelBindingResources.ModelState_NonPropertyAttemptedValueIsInvalid, x]);

            provider.SetNonPropertyUnknownValueIsInvalidAccessor(()
                => localizer[ModelBindingResources.ModelState_NonPropertyUnknownValueIsInvalid]);

            provider.SetUnknownValueIsInvalidAccessor((x)
                => localizer[ModelBindingResources.ModelState_UnknownValueIsInvalid, x]);
        }
    }
}
