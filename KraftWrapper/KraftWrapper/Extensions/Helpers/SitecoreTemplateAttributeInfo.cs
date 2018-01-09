using KraftWrapper.Attributes;
using System;
using System.Collections.Generic;

namespace KraftWrapper.Extensions.Helpers
{
    class SitecoreTemplateAttributeInfo
    {
        public Type Type { get; set; }
        public SitecoreTemplateAttribute SitecoreTemplateAttribute { get; set; }
        public IList<SitecoreFieldAttributeInfo> SitecoreFieldAttributeInfos { get; set; }
    }
}
