using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Armaiti.Core.Localization
{
    /// <summary>
    /// This class represent the most widely used cultural features.
    /// </summary>
    public class RequestUICulture : IRequestUICulture
    {
        public RequestUICulture(IHttpContextAccessor httpContextAccessor)
        {
            Culture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
        }

        /// <summary>
        /// Current UI <see cref="CultureInfo"/>.
        /// </summary>
        public CultureInfo Culture { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="TextInfo"/> object
        ///     represents a writing system where text flows from right to left.
        /// </summary>
        public bool IsRTL => Culture.TextInfo.IsRightToLeft;

        /// <summary>
        /// Gets a value indicating whether the current culture direction.
        /// </summary>
        public string Direction => Culture.TextInfo.IsRightToLeft ? "rtl" : "ltr";

        /// <summary>
        /// Gets the ISO 639-1 two-letter code for the language of the current <see cref="CultureInfo"/>.
        /// </summary>
        public string Language => Culture.TwoLetterISOLanguageName;

        /// <summary>
        /// Gets the culture name in the format languagecode2-country/regioncode2.
        /// </summary>
        public string Name => Culture.Name;
    }
}
