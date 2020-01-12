using System;
using System.Drawing;
using System.Globalization;

namespace Armaiti.Core
{
    /// <summary>
    /// Extension methods to convert color formats such as Hex to Rgb and vice versa.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// This method checks that the color string is hex/html color.
        /// </summary>
        /// <param name="cssColor">The color string.</param>
        /// <returns>Returns true if color string is hex/html color, otherwise false.</returns>
        public static bool IsHtmlColor(this string cssColor)
        {
            return cssColor.StartsWith("#");
        }

        /// <summary>
        /// This method checks that the color string is RGB/ARGB color.
        /// </summary>
        /// <param name="cssColor">The color string.</param>
        /// <returns>Returns true if color string is RGB/ARGB color, otherwise false.</returns>
        public static bool IsRgbColor(this string cssColor)
        {
            return cssColor.StartsWith("rgb"); //rgb or argb
        }

        /// <summary>
        /// Parse the color string, and return the corresponding System.Drawing.Color structure.
        /// </summary>
        /// <param name="cssColor">The color string.</param>
        /// <returns>The System.Drawing.Color structure.</returns>
        public static Color ParseColor(this string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.IsHtmlColor())
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.IsRgbColor())
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    throw new FormatException("rgba format error");

                string noBrackets = cssColor.Substring(left + 1, right - left - 1);
                string[] parts = noBrackets.Split(',');

                int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length == 4)
                {
                    float a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }

            throw new FormatException("Not rgb, rgba or hexa color string");
        }

        /// <summary>
        /// Translates the specified System.Drawing.Color structure to an HTML string color representation.
        /// </summary>
        /// <param name="c">The System.Drawing.Color structure to translate.</param>
        /// <returns>The string that represents the HTML color.</returns>
        public static string ToHtml(this Color c)
        {
            return ColorTranslator.ToHtml(c);
        }
    }
}
