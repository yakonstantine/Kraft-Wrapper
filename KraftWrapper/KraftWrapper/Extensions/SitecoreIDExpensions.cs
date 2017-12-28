using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
