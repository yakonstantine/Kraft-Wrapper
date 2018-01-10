using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;
using System.Web;

namespace KraftWrapper.Core.SitecoreExtensions
{
    public static class FieldExtensions
    {
        public static HtmlString RenderToHtml(this Field field, string parameters = "")
        {
            return new HtmlString(FieldRenderer.Render(field.Item, field.ID.ToString(), parameters));
        }
    }
}
