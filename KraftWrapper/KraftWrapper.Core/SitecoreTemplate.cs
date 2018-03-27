using KraftWrapper.Interfaces;
using Sitecore.Data.Items;
using System;

namespace KraftWrapper.Core
{
    class SitecoreTemplate : ISitecoreTemplate
    {
        private TemplateItem _template;

        public SitecoreTemplate(TemplateItem template)
        {
            _template = template ?? throw new ArgumentNullException("Input sitecore template is null.");
        }

        public Guid Id
        {
            get
            {
                return _template.ID.ToGuid();
            }
        }
    }
}
