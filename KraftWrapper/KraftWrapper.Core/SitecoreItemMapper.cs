using KraftWrapper.Attributes;
using KraftWrapper.Core.Helpers;
using KraftWrapper.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraftWrapper.Core
{
    partial class SitecoreItem
    {
        public T As<T>()
            where T : class, IModel, new()
        {
            return (T)this.As(typeof(T));
        }

        public object As(Type type)
        {
            var sitecoreTemplateAttributeInfo = SitecoreTemplateAttributesCache.TryToGetInfoForAType(type);

            if (!IsValidTemplate(sitecoreTemplateAttributeInfo.SitecoreTemplateAttribute, this.TemplateId, this.TemplateName))
            {
                return null;
            }

            return ConvertItemToModel(this, sitecoreTemplateAttributeInfo);
        }

        private static bool IsValidTemplate(SitecoreTemplateAttribute templateAttribute, Guid templateId, string templateName)
        {
            if (string.IsNullOrEmpty(templateAttribute.TemplateId))
            {
                return templateName == templateAttribute.TemplateName;
            }

            return templateId == new Guid(templateAttribute.TemplateId);
        }

        private static object ConvertItemToModel(ISitecoreItem item, SitecoreTemplateAttributeInfo sitecoreTemplateAttributeInfo)
        {
            var result = Activator.CreateInstance(sitecoreTemplateAttributeInfo.Type);

            ((IModel)result).Id = item.Id;

            foreach (var fieldInfo in sitecoreTemplateAttributeInfo.SitecoreFieldAttributeInfos)
            {
                var propertyInfo = fieldInfo.PropertyInfo;
                var propertyType = propertyInfo.PropertyType;

                if (propertyType.IsGenericEnumerable())
                {
                    var genericArguments = propertyType.GetGenericArguments();

                    var children = ConvertChildren(genericArguments[0], item.GetChildren());

                    if (children != null)
                    {
                        propertyInfo.SetValue(result, children);
                    }

                    continue;
                }

                SetFieldValue(item, result, fieldInfo);
            }

            return result;
        }

        private static IList ConvertChildren(Type childType, IList<ISitecoreItem> children)
        {
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(childType));

            if (!children.Any())
                return list;

            var childrenGroups = children.GroupBy(
                x => new { x.TemplateId, x.TemplateName },
                x => x,
                (key, items) => new { Template = key, Items = items.ToList() });

            var childSitecoreTemplateAttributeInfos = GetTemplateInheritanceList(
                SitecoreTemplateAttributesCache.TryToGetInfoForAType(childType));

            foreach (var childSitecoreTemplateAttributeInfo in childSitecoreTemplateAttributeInfos)
            {
                var childrenGroup = childrenGroups.FirstOrDefault(x =>
                    IsValidTemplate(
                        childSitecoreTemplateAttributeInfo.SitecoreTemplateAttribute,
                        x.Template.TemplateId,
                        x.Template.TemplateName));

                if (childrenGroup == null)
                {
                    continue;
                }

                foreach (var obj in childrenGroup.Items
                    .Select(x => ConvertItemToModel(x, childSitecoreTemplateAttributeInfo)))
                {
                    list.Add(obj);
                }
            }

            return list;
        }

        private static void SetFieldValue(ISitecoreItem item, object targetObject, SitecoreFieldAttributeInfo sitecoreFieldAttributeInfo)
        {
            var value = GetSitecoreItemFieldValue(item, sitecoreFieldAttributeInfo.PropertyInfo.PropertyType, sitecoreFieldAttributeInfo.FieldAttribute);
            sitecoreFieldAttributeInfo.PropertyInfo.SetValue(targetObject, value);
        }

        private static object GetSitecoreItemFieldValue(ISitecoreItem item, Type propertyType, SitecoreFieldAttribute fieldAttribute)
        {
            ISitecoreField field = null;

            if (!string.IsNullOrEmpty(fieldAttribute.FieldId))
            {
                field = item.GetField(new Guid(fieldAttribute.FieldId));
            }
            else if (!string.IsNullOrEmpty(fieldAttribute.FieldName))
            {
                field = item.GetField(fieldAttribute.FieldName);
            }
            else
            {
                field = item.GetField(fieldAttribute.FieldIndex);
            }

            if (field == null)
            {
                return null;
            }

            if (propertyType.IsSimple() || propertyType == typeof(DateTime))
            {
                return GetFieldValueForSimpleType(propertyType, field.Value);
            }

            if (propertyType == typeof(HtmlString))
            {
                return field.RenderToHtml();
            }

            return field.CastToCustomField(propertyType);
        }

        private static object GetFieldValueForSimpleType(Type propertyType, string fieldValue)
        {
            if (propertyType == typeof(string))
            {
                return fieldValue;
            }

            if (propertyType.IsNumeric())
            {
                int parseResult;
                int.TryParse(fieldValue, out parseResult);

                return parseResult;
            }

            if (propertyType.IsFloatingPoint())
            {
                double parseResult;
                double.TryParse(fieldValue, out parseResult);

                return parseResult;
            }

            if (propertyType == typeof(bool))
            {
                var valueLower = fieldValue.ToLower();

                return valueLower == "1" || valueLower == "true";
            }

            if (propertyType == typeof(DateTime))
            {
                return DateTime.Parse(fieldValue);
            }

            return null;
        }

        private static IList<SitecoreTemplateAttributeInfo> GetTemplateInheritanceList(SitecoreTemplateAttributeInfo sitecoreTemplateAttributeInfo)
        {
            var result = new List<SitecoreTemplateAttributeInfo> { sitecoreTemplateAttributeInfo };

            foreach (var derivedTemplate in sitecoreTemplateAttributeInfo.DerivedModelClasses)
            {
                result.AddRange(GetTemplateInheritanceList(derivedTemplate));
            }

            return result;
        }
    }
}
