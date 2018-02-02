using System;
using System.Collections;

namespace KraftWrapper.Core.Helpers
{
    public static class TypeExtensions
    {
        public static bool IsSimple(this Type type)
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

        public static bool IsNumeric(this Type type)
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

        public static bool IsFloatingPoint(this Type type)
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

        public static bool IsGenericEnumerable(this Type type)
        {
            if (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type))
            {
                return true;
            }

            return false;
        }
    }
}
