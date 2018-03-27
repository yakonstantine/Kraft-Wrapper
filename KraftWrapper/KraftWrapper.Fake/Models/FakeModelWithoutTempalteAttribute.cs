using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System;

namespace KraftWrapper.Fake.Models
{
    public class FakeModelWithoutTempalteAttribute : IModel
    {
        public Guid Id { get; set; }

        [SitecoreField(FieldId = IDsForModelWithoutTempalteAttribute.TextValueFieldId)]
        public string TextValue { get; set; }
    }
}
