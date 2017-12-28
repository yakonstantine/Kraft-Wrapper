using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using KraftWrapper.Interfaces.Fields;
using System;
using System.Web;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = IDsForModelWithAllFieldTypes.TemplateId)]
    public class FakeModelWithAllFieldTypes : ISitecoreTemplate
    {
        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.TextValueFieldId)]
        public string TextValue { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.IntegerValueFieldId)]
        public int IntegerValue { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.DoubleValueFieldId)]
        public double DoubleValue { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.DateTimeValueFieldId)]
        public DateTime DateTimeValue { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.HtmlStringValueFieldId)]
        public HtmlString HtmlStringValue { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.TextFieldId)]
        public ISitecoreTextField TextField { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.LinkFieldId)]
        public ISitecoreLinkField LinkField { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.CheckboxFieldId)]
        public ISitecoreCheckboxField CheckboxField { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.ImageFieldId)]
        public ISitecoreImageField ImageField { get; set; }

        [SitecoreField(FieldId = IDsForModelWithAllFieldTypes.InternalLinkFieldId)]
        public ISitecoreInternalLinkField InternalLinkField { get; set; }
    }
}
