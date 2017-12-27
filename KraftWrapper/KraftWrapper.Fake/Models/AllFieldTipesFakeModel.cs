using KraftWrapper.Attributes;
using KraftWrapper.Fake.IDsStorage;
using KraftWrapper.Interfaces;
using KraftWrapper.Interfaces.Fields;
using System;
using System.Web;

namespace KraftWrapper.Fake.Models
{
    [SitecoreTemplate(TemplateId = AllFieldTipesTemplate.TemplateId)]
    public class AllFieldTipesFakeModel : ISitecoreTemplate
    {
        [SitecoreField(FieldId = AllFieldTipesTemplate.TextValueFieldId)]
        public string TextValue { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.IntegerValueFieldId)]
        public int IntegerValue { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.BooleanValueFieldId)]
        public bool BooleanValue { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.DoubleValueFieldId)]
        public double DoubleValue { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.DateTimeValueFieldId)]
        public DateTime DateTimeValue { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.HtmlStringValueFieldId)]
        public HtmlString HtmlStringValue { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.TextFieldId)]
        public ISitecoreTextField TextField { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.LinkFieldId)]
        public ISitecoreLinkField LinkField { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.CheckboxFieldId)]
        public ISitecoreCheckboxField CheckboxField { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.ImageFieldId)]
        public ISitecoreImageField ImageField { get; set; }

        [SitecoreField(FieldId = AllFieldTipesTemplate.InternalLinkFieldId)]
        public ISitecoreInternalLinkField InternalLinkField { get; set; }
    }
}
