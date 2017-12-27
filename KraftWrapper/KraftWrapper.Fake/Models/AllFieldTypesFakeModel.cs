using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using KraftWrapper.Interfaces.Fields;
using System;
using System.Web;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = AllFieldTypesTemplate.TemplateId)]
    public class AllFieldTypesFakeModel : ISitecoreTemplate
    {
        [SitecoreField(FieldId = AllFieldTypesTemplate.TextValueFieldId)]
        public string TextValue { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.IntegerValueFieldId)]
        public int IntegerValue { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.DoubleValueFieldId)]
        public double DoubleValue { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.DateTimeValueFieldId)]
        public DateTime DateTimeValue { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.HtmlStringValueFieldId)]
        public HtmlString HtmlStringValue { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.TextFieldId)]
        public ISitecoreTextField TextField { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.LinkFieldId)]
        public ISitecoreLinkField LinkField { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.CheckboxFieldId)]
        public ISitecoreCheckboxField CheckboxField { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.ImageFieldId)]
        public ISitecoreImageField ImageField { get; set; }

        [SitecoreField(FieldId = AllFieldTypesTemplate.InternalLinkFieldId)]
        public ISitecoreInternalLinkField InternalLinkField { get; set; }
    }
}
