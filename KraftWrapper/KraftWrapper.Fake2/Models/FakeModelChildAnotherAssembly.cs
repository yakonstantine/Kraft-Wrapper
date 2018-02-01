using KraftWrapper.Attributes;
using KraftWrapper.Fake.Models;
using KraftWrapper.Fake2.IDsStorage;

namespace KraftWrapper.Fake2.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelChildAnotherAssembly.TemplateId)]
    public class FakeModelChildAnotherAssembly : FakeModelWithTwoFields
    {
        [SitecoreField(FieldId = IDsForModelChildAnotherAssembly.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }
    }
}
