using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace Armaiti.Core.Controllers
{
    public class LanguageController : Controller
    {
        /// <summary>
        /// Sets the language of the site to the user's preferred language.
        /// </summary>
        /// <param name="lcid">The culture identifier.</param>
        /// <param name="returnUrl">After changing the language, the user will be redirected to this URL.</param>
        /// <returns>Redirect to <paramref name="returnUrl"/>.</returns>
        [HttpPost]
        public IActionResult Set(short lcid = 0, string returnUrl = "~/")
        {
            var culture = new CultureInfo(lcid);

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
