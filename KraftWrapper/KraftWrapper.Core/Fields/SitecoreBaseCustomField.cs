using KraftWrapper.Core.SitecoreExtensions;
using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;
using System;
using System.Web;

namespace KraftWrapper.Core.Fields
{
    abstract class SitecoreBaseCustomField<T> : ISitecoreBaseCustomField
        where T : CustomField
    {
        protected readonly T _field;

        protected SitecoreBaseCustomField(T field)
        {
            _field = field ?? throw new ArgumentNullException($"Input field for type {nameof(T)} is null.");
        }

        public string Value
        {
            get
            {
                return _field.Value;
            }
        }

        public HtmlString RenderToHtml(string parameters = "")
        {
            return _field.InnerField.RenderToHtml(parameters);
        }
    }
}
