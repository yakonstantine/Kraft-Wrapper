using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelChild2.TemplateId)]
    public class FakeModelChild2 : FakeModelWithTwoFields
    {
        [SitecoreField(FieldId = IDsForModelChild2.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }
    }
}
