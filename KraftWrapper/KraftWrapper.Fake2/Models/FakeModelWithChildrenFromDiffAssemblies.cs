using KraftWrapper.Attributes;
using KraftWrapper.Fake.Models;
using KraftWrapper.Fake2.IDsStorage;
using KraftWrapper.Interfaces;
using System;
using System.Collections.Generic;

namespace KraftWrapper.Fake2.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithChildrenFromDiffAssemblies.TemplateId)]
    public class FakeModelWithChildrenFromDiffAssemblies : ISitecoreTemplate
    {
        public Guid Id { get; set; }

        [SitecoreField(FieldId = IDsForModelWithChildrenFromDiffAssemblies.TextValueFieldId)]
        public string TextValue { get; set; }

        public IList<FakeModelWithTwoFields> Children { get; set; }
    }
}
