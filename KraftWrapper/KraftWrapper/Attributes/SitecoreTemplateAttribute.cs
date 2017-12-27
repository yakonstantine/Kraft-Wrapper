using System;

namespace KraftWrapper.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SitecoreTemplateAttribute : Attribute
    {
        public string TemplateId { get; set; }
    }
}
