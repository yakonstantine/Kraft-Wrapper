using KraftWrapper.Interfaces;
using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;
using Sitecore.Links;

namespace KraftWrapper.Core.Fields
{
    public class SitecoreInternalLinkField : SitecoreBaseCustomField<InternalLinkField>, ISitecoreInternalLinkField
    {
        public SitecoreInternalLinkField(InternalLinkField field) : base(field)
        {
        }

        public string Path { get { return _field.Path; } }

        public string ItemUrl
        {
            get
            {
                if (_field.TargetItem == null)
                {
                    return null;
                }

                var urlOptions = UrlOptions.DefaultOptions;
                urlOptions.AlwaysIncludeServerUrl = false;
                urlOptions.SiteResolving = true;

                return LinkManager.GetItemUrl(_field.TargetItem, urlOptions); ;
            }
        }

        public ISitecoreItem TargetItem
        {
            get
            {
                return _field.TargetItem != null
                            ? new SitecoreItem(_field.TargetItem)
                            : null;
            }
        }
    }
}
