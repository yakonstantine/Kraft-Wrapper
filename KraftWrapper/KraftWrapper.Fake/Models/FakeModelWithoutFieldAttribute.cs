﻿using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithoutFieldAttribute.TemplateId)]
    public class FakeModelWithoutFieldAttribute : ISitecoreTemplate
    {
        [SitecoreField(FieldId = IDsForModelWithoutFieldAttribute.TextValueFieldId)]
        public string TextValue { get; set; }

        public int IntegerValue { get; set; }
    }
}
