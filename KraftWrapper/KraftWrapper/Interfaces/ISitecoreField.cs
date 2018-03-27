using KraftWrapper.Interfaces.Fields;
using System;
using System.Web;

namespace KraftWrapper.Interfaces
{
    public interface ISitecoreField
    {
        string Source { get; }
        string Value { get; set; }
        DateTime DateTime { get; }
        T CastToCustomField<T>() where T : class, ISitecoreBaseCustomField;
        object CastToCustomField(Type type);
        HtmlString RenderToHtml(string parameters = "");
    }
}
