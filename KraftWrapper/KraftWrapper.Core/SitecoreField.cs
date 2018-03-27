using KraftWrapper.Core.Fields;
using KraftWrapper.Core.SitecoreExtensions;
using KraftWrapper.Interfaces;
using KraftWrapper.Interfaces.Fields;
using Sitecore.Data.Fields;
using System;
using System.Web;

namespace KraftWrapper.Core
{
    class SitecoreField : ISitecoreField
    {
        private readonly Field _field;

        public SitecoreField(Field field)
        {
            _field = field ?? throw new ArgumentNullException("Input field is null.");
        }

        public string Source
        {
            get
            {
                return _field.Source;
            }
        }

        public string Value
        {
            get { return _field.Value; }
            set { _field.Value = value; }
        }

        public DateTime DateTime
        {
            get { return ((DateField)_field).DateTime; }
        }

        public object CastToCustomField(Type type)
        {
            if ((!type.IsClass && !type.IsInterface) || !typeof(ISitecoreBaseCustomField).IsAssignableFrom(type))
                throw new ArgumentException($"Type {type.Name} is not a class or is not inherit from ISitecoreBaseCustomField.");

            if (type == typeof(ISitecoreLinkField))
                return new SitecoreLinkField(_field);

            if (type == typeof(ISitecoreImageField))
                return new SitecoreImageField(_field);

            if (type == typeof(ISitecoreCheckboxField))
                return new SitecoreCheckboxField(_field);

            if (type == typeof(ISitecoreInternalLinkField))
                return new SitecoreInternalLinkField(_field);

            if (type == typeof(ISitecoreTextField))
                return new SitecoreTextField(_field);

            throw new NotImplementedException($"Resolving for the {type.Name} is not implemented.");
        }

        public T CastToCustomField<T>()
            where T : class, ISitecoreBaseCustomField
        {
            return (T)CastToCustomField(typeof(T));
        }

        public HtmlString RenderToHtml(string parameters = "")
        {
            return _field.RenderToHtml(parameters);
        }
    }
}
