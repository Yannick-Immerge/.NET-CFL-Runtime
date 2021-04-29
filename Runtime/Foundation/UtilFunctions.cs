using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Runtime.Foundation
{
    public static class UtilFunctions
    {
        public static bool EvaluateBoolExpression(this string s)
        {
            //Evaluate subexpressions
            Stack<int> p = new Stack<int>();
            for (int i = s.Length - 1; i >= 0; i--)
                if (s[i] == ')')
                    p.Push(i);
                else if (s[i] == '(')
                {
                    string sub = s.Substring(i, p.Peek() - i + 1);
                    s = s.Remove(i, p.Pop() - i + 1).Insert(i, sub.Substring(1, sub.Length -2).EvaluateBoolExpression().ToString().ToLower());
                }

            //Comp operators
            while (true)
            {
                Match m = Regex.Match(s, @"([0-9],?[0-9]*)\s*(\>\=|\<\=|\>|\<|==|!=)\s*([0-9],?[0-9]*)");
                if (!m.Success)
                    break;
                dynamic a = m.Groups[1].Value.EvaluateMathExpression();
                dynamic b = m.Groups[3].Value.EvaluateMathExpression();
                string ins = null;
                switch (m.Groups[2].Value)
                {
                    case ">=":
                        ins = (a >= b).ToString().ToLower();
                        break;
                    case "<=":
                        ins = (a <= b).ToString().ToLower();
                        break;
                    case ">":
                        ins = (a > b).ToString().ToLower();
                        break;
                    case "<":
                        ins = (a < b).ToString().ToLower();
                        break;
                    case "==":
                        ins = (a == b).ToString().ToLower();
                        break;
                    case "!=":
                        ins = (a != b).ToString().ToLower();
                        break;
                }
                s = s.Remove(m.Index, m.Length).Insert(m.Index, ins);
            }

            //First operators
            while (true)
            {
                Match m = Regex.Match(s, @"(true|false)\s*(&&|\|\|)\s*(true|false)");
                if (!m.Success)
                    break;
                bool a = m.Groups[1].Value == "true";
                bool b = m.Groups[3].Value == "true";
                s = s.Remove(m.Index, m.Length).Insert(m.Index, m.Groups[2].Value == "&&" ? (a && b).ToString().ToLower() : (a || b).ToString().ToLower());
            }

            //Second operators

            return s == "true";
        }

        public static object EvaluateMathExpression(this string s)
            => new NCalc.Expression(s.Replace(',', '.')).Evaluate();

        public static CFLType GetCFLType(this object o)
        {
            if (o is StructureObject s)
                return s.Type;
            return null;
        }
        
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
