using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System.Collections.Generic;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = TemplateWithChildren.TemplateId)]
    public class TemplateWithChildrenFakeModel : ISitecoreTemplate
    {
        [SitecoreField(FieldId = TemplateWithChildren.TextValueId)]
        public string TextValue { get; set; }

        public IList<AllFieldTipesFakeModel> Children { get; set; }
    }
}
