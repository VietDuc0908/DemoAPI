using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS.Common.Extentions
{
    public static class TypeExtensions
    {
        public static bool IsNumericType(this object o)
        {
            var lstNumericType = new List<Type>()
            {
                typeof(Byte),
                typeof(SByte),
                typeof(UInt16),
                typeof(UInt32),
                typeof(UInt64),
                typeof(Int16),
                typeof(Int32),
                typeof(Int64),
                typeof(Decimal),
                typeof(Double),
                typeof(Single),
                typeof(Byte?),
                typeof(SByte?),
                typeof(UInt16?),
                typeof(UInt32?),
                typeof(UInt64?),
                typeof(Int16?),
                typeof(Int32?),
                typeof(Int64?),
                typeof(Decimal?),
                typeof(Double?),
                typeof(Single?)
            };
            var type = ((PropertyInfo)o).PropertyType;
            return lstNumericType.Any(s => s == type);
        }

        public static object GetValueNumeric(this object o, string value)
        {
            var type = ((PropertyInfo)o).PropertyType;
            if (type == typeof(Byte) || type == typeof(Byte?))
            {
            }
            if (type == typeof(SByte) || type == typeof(SByte?))
            {
            }
            if (type == typeof(UInt16) || type == typeof(UInt16?))
            {
            }
            if (type == typeof(UInt32) || type == typeof(UInt32?))
            {
            }
            if (type == typeof(UInt64) || type == typeof(UInt64?))
            {
            }
            if (type == typeof(Int16) || type == typeof(Int16?))
            {
                return value.ToShort();
            }
            if (type == typeof(Int32) || type == typeof(Int32?))
            {
                return value.ToInt();
            }
            if (type == typeof(Int64) || type == typeof(Int64?))
            {
                return value.ToLong();
            }
            if (type == typeof(Decimal) || type == typeof(Decimal?))
            {
                return value.ToDecimal();
            }
            if (type == typeof(Double) || type == typeof(Double?))
            {
                return value.ToDouble();
            }
            if (type == typeof(Single) || type == typeof(Single?))
            {
                return value.ToSingle();
            }
            return null;
        }

        public static bool IsDateType(this object o)
        {
            var lstType = new List<Type>()
            {
                typeof(DateTime?),
                typeof(DateTime)
            };
            var type = ((PropertyInfo)o).PropertyType;
            return lstType.Any(s => s == type);
        }

        public static bool IsBoolType(this object o)
        {
            var lstType = new List<Type>()
            {
                typeof(bool?),
                typeof(bool)
            };
            var type = ((PropertyInfo)o).PropertyType;
            return lstType.Any(s => s == type);
        }


        public static PropertyInfo GetProperty<T>(this T obj, string name) where T : class
        {
            var fullName = obj.JsonSerialize().Replace("\"", "");
            var t = Type.GetType(fullName);
            var val = System.Reflection.TypeExtensions.GetProperty(t, name);

            return val;
        }
    }
}
