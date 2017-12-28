using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;

namespace KraftWrapper.Core.Fields
{
    public class SitecoreTextField : SitecoreBaseCustomField<TextField>, ISitecoreTextField
    {
        public SitecoreTextField(TextField field) : base(field)
        {
        }
    }
}
