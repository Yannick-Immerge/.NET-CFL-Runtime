using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public static class UtilFunctions
    {
        public static bool IsNumericType(this object o)
            => IsNumericType(o, out _);

        public static bool IsNumericType(this object o, out dynamic v) 
        { 
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                    v = (byte)o;
                    return true;
                case TypeCode.SByte:
                    v = (sbyte)o;
                    return true;
                case TypeCode.UInt16:
                    v = (ushort)o;
                    return true;
                case TypeCode.UInt32:
                    v = (uint)o;
                    return true;
                case TypeCode.UInt64:
                    v = (ulong)o;
                    return true;
                case TypeCode.Int16:
                    v = (short)o;
                    return true;
                case TypeCode.Int32:
                    v = (int)o;
                    return true;
                case TypeCode.Int64:
                    v = (long)o;
                    return true;
                case TypeCode.Decimal:
                    v = (decimal)o;
                    return true;
                case TypeCode.Double:
                    v = (double)o;
                    return true;
                case TypeCode.Single:
                    v = (float)o;
                    return true;
                default:
                    v = null;
                    return false;
            }
        }
    }
}
