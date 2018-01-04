using System;

namespace KraftWrapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class SitecoreFieldAttribute : Attribute
    {
        public string FieldId { get; set; }
        public string FieldName { get; set; }
        public int FieldIndex { get; set; }
    }
}
