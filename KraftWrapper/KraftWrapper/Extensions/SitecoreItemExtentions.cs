﻿using KraftWrapper.Attributes;
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
            var templateId = ValidateTypeAndGetTemplateID(type);

            if (item.TemplateId != templateId)
                return null;

            var fieldInfos = GetFieldInfos(type);

            return item.ConvertTo(type, fieldInfos);
        }

        private static Guid ValidateTypeAndGetTemplateID(Type type)
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

            return new Guid(sitecoreTemplateAttribute.TemplateId);
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
                    FieldId = new Guid(sitecoreFieldAttribute.FieldId)
                });
            }

            return result;
        }

        private static IList ConvertChildren(Type childType, IList<ISitecoreItem> children)
        {
            var childTemplateId = ValidateTypeAndGetTemplateID(childType);

            var childrenGroups = children.GroupBy(
                x => x.TemplateId,
                x => x,
                (key, items) => new { TemplateId = key, Items = items.ToList() });

            var childrenGroup = childrenGroups.FirstOrDefault(x => x.TemplateId == childTemplateId);

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
            var value = GetFieldValue(fieldInfo.PropertyInfo.PropertyType, item, fieldInfo.FieldId);
            fieldInfo.PropertyInfo.SetValue(targetObject, value);
        }

        private static object GetFieldValue(Type propertyType, ISitecoreItem item, Guid fieldId)
        {
            var field = item.GetField(fieldId);

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
            public Guid FieldId { get; set; }
        }
    }
}
