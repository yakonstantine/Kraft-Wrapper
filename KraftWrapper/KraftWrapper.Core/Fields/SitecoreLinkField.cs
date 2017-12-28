using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;

namespace KraftWrapper.Core.Fields
{
    class SitecoreLinkField : SitecoreBaseCustomField<LinkField>, ISitecoreLinkField
    {
        public SitecoreLinkField(LinkField field) : base(field)
        {
        }

        public string FriendlyUrl
        {
            get
            {
                return _field.GetFriendlyUrl();
            }
        }

        public string Text
        {
            get
            {
                return _field.Text;
            }
        }
    }
}
