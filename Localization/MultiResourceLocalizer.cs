using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Armaiti.Core.Localization
{
    /// <summary>
    /// Service to enable localization for application with multiple resource sources.
    /// </summary>
    public class MultiResourceLocalizer : IMultiResourceLocalizer
    {
        private readonly IList<IStringLocalizer> _localizers;

        public MultiResourceLocalizer(IStringLocalizerFactory factory, Type primaryResource, params Type[] otherResouces)
        {
            _localizers = new List<IStringLocalizer>();
            _localizers.Add(factory.Create(primaryResource));

            foreach (var resource in otherResouces)
            {
                var localizer = factory.Create(resource);
                _localizers.Add(localizer);
            }
        }

        public LocalizedString this[string name]
        {
            get
            {
                for (int i = 0; i < _localizers.Count; i++)
                {
                    var result = _localizers[i].GetString(name);
                    if (!result.ResourceNotFound)
                        return result;
                }
                return new LocalizedString(name, name, true);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                for (int i = 0; i < _localizers.Count; i++)
                {
                    var result = _localizers[i].GetString(name, arguments);
                    if (!result.ResourceNotFound)
                        return result;
                }
                return new LocalizedString(name, string.Format(name, arguments), true);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var result = _localizers[0].GetAllStrings(includeParentCultures);

            foreach (var item in result)
            {
                if (item.ResourceNotFound)
                {
                    for (int i = 1; i < _localizers.Count; i++)
                    {
                        var locStr = _localizers[i].GetString(item.Name);
                        if (!locStr.ResourceNotFound)
                            yield return locStr;
                    }
                }

                yield return item;
            }
        }

        [Obsolete("This method is obsolete. Use `CurrentCulture` and `CurrentUICulture` instead.")]
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return _localizers[0].WithCulture(culture);
        }

        [Obsolete("This method is obsolete. Use `CurrentCulture` and `CurrentUICulture` instead.")]
        public IStringLocalizer WithCulture(int i, CultureInfo culture)
        {
            return _localizers[i].WithCulture(culture);
        }
    }
}
