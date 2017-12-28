using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System.Collections.Generic;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithTwoChildrenLists.TemplateId)]
    public class FakeModelWithTwoChildrenLists : ISitecoreTemplate
    {
        [SitecoreField(FieldId = IDsForModelWithTwoChildrenLists.TextValueId)]
        public string TextValue { get; set; }

        public IList<FakeModelWithAllFieldTypes> ChildrenWithAllFields { get; set; }

        public IList<FakeModelWithTwoFields> ChildrenWithTwoFields { get; set; }
    }
}
