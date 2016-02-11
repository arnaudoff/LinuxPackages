namespace LinuxPackages.Web.Mvc.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public static class Conversions
    {
        public static IEnumerable<SelectListItem> EnumToSelectList<T>() where T : struct, IComparable, IConvertible, IFormattable
        {
            return (Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem() { Text = e.ToString(), Value = e.GetHashCode().ToString() }))
                .ToList();
        }
    }
}