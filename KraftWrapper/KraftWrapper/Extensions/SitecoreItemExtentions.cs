using KraftWrapper.Attributes;
using KraftWrapper.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace KraftWrapper.Extensions
{
    public static class SitecoreItemExtentions
    {
        public static T As<T>(this ISitecoreItem item)
            where T : class, ISitecoreTemplate, new()
        {
            return (T)item.As(typeof(T));
        }

        public static object As(this ISitecoreItem item, Type type)
        {
            var templateAttribute = ValidateTypeAndGetTemplateAttribute(type);

            if (!IsValidTemplate(templateAttribute, item.TemplateId, item.TemplateName))
            {
                return null;
            }

            var fieldInfos = GetFieldInfos(type);

            return item.ConvertTo(type, fieldInfos);
        }

        private static SitecoreTemplateAttribute ValidateTypeAndGetTemplateAttribute(Type type)
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

        private static object ConvertTo(this ISitecoreItem item, Type type, IList<FieldInfo> fieldInfos)
        {
            var result = Activator.CreateInstance(type);

            foreach (var fieldInfo in fieldInfos)
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

                SetFieldValue(result, item, fieldInfo);
            }

            return result;
        }

        private static IList<FieldInfo> GetFieldInfos(Type type)
        {
            var result = new List<FieldInfo>();

            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType != typeof(string)
                    && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    result.Add(new FieldInfo
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

                result.Add(new FieldInfo
                {
                    PropertyInfo = propertyInfo,
                    FieldAttribute = sitecoreFieldAttribute
                });
            }

            return result;
        }

        private static IList ConvertChildren(Type childType, IList<ISitecoreItem> children)
        {
            var childTemplateAttribute = ValidateTypeAndGetTemplateAttribute(childType);

            var childrenGroups = children.GroupBy(
                x => new { x.TemplateId, x.TemplateName },
                x => x,
                (key, items) => new { Template = key, Items = items.ToList() });

            var childrenGroup = childrenGroups.FirstOrDefault(x => IsValidTemplate(childTemplateAttribute, x.Template.TemplateId, x.Template.TemplateName));

            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(childType));

            if (childrenGroup == null)
                return list;

            var childfieldInfos = GetFieldInfos(childType);

            foreach (var obj in childrenGroup.Items
                .Select(x => x.ConvertTo(childType, childfieldInfos)))
            {
                list.Add(obj);
            }

            return list;
        }

        private static void SetFieldValue(object targetObject, ISitecoreItem item, FieldInfo fieldInfo)
        {
            var value = GetFieldValue(fieldInfo.PropertyInfo.PropertyType, item, fieldInfo.FieldAttribute);
            fieldInfo.PropertyInfo.SetValue(targetObject, value);
        }

        private static object GetFieldValue(Type propertyType, ISitecoreItem item, SitecoreFieldAttribute fieldAttribute)
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
                return null;

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

        private static bool IsValidTemplate(SitecoreTemplateAttribute templateAttribute, Guid templateId, string templateName)
        {
            if (string.IsNullOrEmpty(templateAttribute.TemplateId))
            {
                return templateName == templateAttribute.TemplateName;
            }

            return templateId == new Guid(templateAttribute.TemplateId);
        }

        private static bool IsSimple(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsSimple(type.GetGenericArguments()[0]);
            }

            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal));
        }

        private static bool IsNumeric(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsFloatingPoint(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsGenericEnumerable(this Type type)
        {
            if (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type))
            {
                return true;
            }

            return false;
        }

        private class FieldInfo
        {
            public PropertyInfo PropertyInfo { get; set; }
            public SitecoreFieldAttribute FieldAttribute { get; set; }
        }
    }
}
