using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System.Collections.Generic;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithChildList.TemplateId)]
    public class FakeModelWithOneChildList : ISitecoreTemplate
    {
        [SitecoreField(FieldId = IDsForModelWithChildList.TextValueId)]
        public string TextValue { get; set; }

        public IList<FakeModelWithAllFieldTypes> Children { get; set; }
    }
}
