using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreLanguage
    {
        CultureInfo CultureInfo { get; }
        string Name { get; }
    }
}
