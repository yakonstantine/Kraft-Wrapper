using KraftWrapper.Attributes;
using System.Reflection;

namespace KraftWrapper.Core.Helpers
{
    class SitecoreFieldAttributeInfo
    {
        public PropertyInfo PropertyInfo { get; set; }
        public SitecoreFieldAttribute FieldAttribute { get; set; }
    }
}
