using KraftWrapper.Attributes;
using KraftWrapper.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KraftWrapper.Helpers
{
    static class SitecoreTemplateAttributesCache
    {
        private static readonly IDictionary<Type, SitecoreTemplateAttributeInfo> _modelAttributesCache
            = new Dictionary<Type, SitecoreTemplateAttributeInfo>();

        public static SitecoreTemplateAttributeInfo TryToGetInfoForAType(Type type)
        {
            if (!_modelAttributesCache.ContainsKey(type))
            {
                var sitecoreTemplateAttributeInfo = new SitecoreTemplateAttributeInfo
                {
                    Type = type,
                    SitecoreTemplateAttribute = GetSitecoreTemplateAttribute(type),
                    SitecoreFieldAttributeInfos = GetSitecoreFieldAttributeInfos(type)
                };

                _modelAttributesCache.Add(type, sitecoreTemplateAttributeInfo);
            }

            return _modelAttributesCache[type];
        }

        private static IList<SitecoreFieldAttributeInfo> GetSitecoreFieldAttributeInfos(Type type)
        {
            var result = new List<SitecoreFieldAttributeInfo>();

            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType != typeof(string)
                    && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    result.Add(new SitecoreFieldAttributeInfo
                    {
                        PropertyInfo = propertyInfo
                    });

                    continue;
                }

                var sitecoreFieldAttribute = ((SitecoreFieldAttribute[])propertyInfo
                .GetCustomAttributes(typeof(SitecoreFieldAttribute), false))
                .FirstOrDefault();

                if (sitecoreFieldAttribute == null)
                {
                    throw new ArgumentNullException("SitecoreFieldAttribute was not found.");
                }

                result.Add(new SitecoreFieldAttributeInfo
                {
                    PropertyInfo = propertyInfo,
                    FieldAttribute = sitecoreFieldAttribute
                });
            }

            return result;
        }

        private static SitecoreTemplateAttribute GetSitecoreTemplateAttribute(Type type)
        {
            if ((!type.IsClass && !type.IsInterface)
                || !typeof(ISitecoreTemplate).IsAssignableFrom(type)
                || type.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ArgumentException($"Type {type.Name} is not a class or is not inherit from ISitecoreTemplate or does not have default constructor.");
            }

            var sitecoreTemplateAttribute = (SitecoreTemplateAttribute)Attribute
                .GetCustomAttribute(type, typeof(SitecoreTemplateAttribute));

            if (sitecoreTemplateAttribute == null)
            {
                throw new ArgumentNullException("SitecoreTemplateAttribute was not found.");
            }

            return sitecoreTemplateAttribute;
        }
    }
}
