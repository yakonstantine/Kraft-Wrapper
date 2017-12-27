using System.Web;

namespace KraftWrapper.Interfaces.Fields
{
    public interface ISitecoreBaseCustomField
    {
        string Value { get; }
        HtmlString RenderToHtml(string parameters = "");
    }
}
