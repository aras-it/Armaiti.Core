using System;
using System.Globalization;

namespace Armaiti.Core.Localization
{
    /// <summary>
    /// Represent the widely used cultural features.
    /// </summary>
    public interface IRequestUICulture
    {
        /// <summary>
        /// Current UI <see cref="CultureInfo"/>.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="TextInfo"/> object
        ///     represents a writing system where text flows from right to left.
        /// </summary>
        Boolean IsRTL { get; }

        /// <summary>
        /// Gets a value indicating whether the current culture direction.
        /// </summary>
        String Direction { get; }

        /// <summary>
        /// Gets the ISO 639-1 two-letter code for the language of the current <see cref="CultureInfo"/>.
        /// </summary>
        String Language { get; }

        /// <summary>
        /// Gets the culture name in the format languagecode2-country/regioncode2.
        /// </summary>
        String Name { get; }
    }
}
