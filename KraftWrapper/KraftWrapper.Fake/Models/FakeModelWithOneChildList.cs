using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using System;
using System.Collections.Generic;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithChildList.TemplateId)]
    public class FakeModelWithOneChildList : ISitecoreTemplate
    {
        public Guid Id { get; set; }

        [SitecoreField(FieldId = IDsForModelWithChildList.TextValueId)]
        public string TextValue { get; set; }

        public IList<FakeModelWithAllFieldTypes> Children { get; set; }
    }
}
