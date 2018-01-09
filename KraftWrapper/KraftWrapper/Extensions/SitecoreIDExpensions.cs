using System;

namespace KraftWrapper.Extensions
{
    public static class SitecoreIDExpensions
    {
        public static string ToIDString(this Guid id)
        {
            return $"{{{id.ToString().ToUpper()}}}";
        }
    }
}
