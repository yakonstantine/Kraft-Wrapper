using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelChild3.TemplateId)]
    public class FakeModelChild3 : FakeModelWithTwoFields
    {
        [SitecoreField(FieldId = IDsForModelChild3.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }
    }
}
