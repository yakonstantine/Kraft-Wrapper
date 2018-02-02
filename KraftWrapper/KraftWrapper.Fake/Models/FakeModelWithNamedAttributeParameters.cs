using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateName = IDsForModelWithNamedAttributeParameters.TemplateName)]
    public class FakeModelWithNamedAttributeParameters : ISitecoreTemplate
    {
        public Guid Id { get; set; }

        [SitecoreField(FieldName = IDsForModelWithNamedAttributeParameters.TextValueFieldName)]
        public string TextValue { get; set; }

        [SitecoreField(FieldIndex = IDsForModelWithNamedAttributeParameters.IntegerValueFieldIndex)]
        public int IntegerValue { get; set; }
    }
}
