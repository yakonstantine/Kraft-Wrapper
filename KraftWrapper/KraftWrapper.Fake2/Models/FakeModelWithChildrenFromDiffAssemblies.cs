using KraftWrapper.Attributes;
using KraftWrapper.Fake.Models;
using KraftWrapper.Fake2.IDsStorage;
using KraftWrapper.Interfaces;
using System.Collections.Generic;

namespace KraftWrapper.Fake2.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithChildrenFromDiffAssemblies.TemplateId)]
    public class FakeModelWithChildrenFromDiffAssemblies : ISitecoreTemplate
    {
        [SitecoreField(FieldId = IDsForModelWithChildrenFromDiffAssemblies.TextValueFieldId)]
        public string TextValue { get; set; }

        public IList<FakeModelWithTwoFields> Children { get; set; }
    }
}
