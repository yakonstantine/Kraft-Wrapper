using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelChild.TemplateId)]
    public class FakeModelChild : FakeModelWithTwoFields
    {
        [SitecoreField(FieldId = IDsForModelChild.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }
    }
}
