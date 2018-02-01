using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelChild.TemplateId)]
    public class FakeModelChild : FakeModelWithTwoFields
    {
        [SitecoreField(FieldId = IDsForModelChild.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }
    }
}
