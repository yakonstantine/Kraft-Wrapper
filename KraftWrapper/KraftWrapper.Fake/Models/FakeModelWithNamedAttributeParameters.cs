using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateName = IDsForModelWithNamedAttributeParameters.TemplateName)]
    public class FakeModelWithNamedAttributeParameters : ISitecoreTemplate
    {
        [SitecoreField(FieldName = IDsForModelWithNamedAttributeParameters.TextValueFieldName)]
        public string TextValue { get; set; }

        [SitecoreField(FieldIndex = IDsForModelWithNamedAttributeParameters.IntegerValueFieldIndex)]
        public int IntegerValue { get; set; }
    }
}
