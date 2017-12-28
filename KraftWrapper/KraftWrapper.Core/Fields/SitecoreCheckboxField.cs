using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;

namespace KraftWrapper.Core.Fields
{
    public class SitecoreCheckboxField : SitecoreBaseCustomField<CheckboxField>, ISitecoreCheckboxField
    {
        public SitecoreCheckboxField(CheckboxField field) : base(field)
        {
        }

        public bool IsChecked
        {
            get
            {
                return _field.Checked;
            }
        }
    }
}
