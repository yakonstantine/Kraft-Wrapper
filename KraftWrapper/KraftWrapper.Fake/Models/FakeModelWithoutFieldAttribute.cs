using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithoutFieldAttribute.TemplateId)]
    public class FakeModelWithoutFieldAttribute : IModel
    {
        public Guid Id { get; set; }

        [SitecoreField(FieldId = IDsForModelWithoutFieldAttribute.TextValueFieldId)]
        public string TextValue { get; set; }

        public int IntegerValue { get; set; }
    }
}
