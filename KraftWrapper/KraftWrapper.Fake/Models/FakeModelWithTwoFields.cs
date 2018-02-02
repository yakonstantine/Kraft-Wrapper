using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithTwoFields.TemplateId)]
    public class FakeModelWithTwoFields : ISitecoreTemplate
    {
        public Guid Id { get; set; }

        [SitecoreField(FieldId = IDsForModelWithTwoFields.TextValueFieldId)]
        public string TextValue { get; set; }

        [SitecoreField(FieldId = IDsForModelWithTwoFields.IntegerValueFieldId)]
        public int IntegerValue { get; set; }
    }
}
