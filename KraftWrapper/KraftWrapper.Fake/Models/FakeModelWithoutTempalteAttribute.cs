using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;

namespace KraftWrapper.Fake.Models
{
    public class FakeModelWithoutTempalteAttribute : ISitecoreTemplate
    {
        [SitecoreField(FieldId = IDsForModelWithoutTempalteAttribute.TextValueFieldId)]
        public string TextValue { get; set; }
    }
}
