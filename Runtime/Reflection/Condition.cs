using Microsoft.VisualBasic;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public class Condition : IValuable
    {
        public ConditionType Type { get; }
        public object A { get; }
        public object B { get; }

        public object GetValue()
        {
            if (A == null)
                throw new InvalidOperationException("Cannot evaluate condition with null-values.");

            object aValue;

            //Get first value object
            if (A is IValuable vA)
                aValue = vA.GetValue();
            else
                aValue = A;

            //Check single parameter
            if(aValue is bool direct)
            {
                if (Type == ConditionType.Direct)
                    return direct;
                else if(Type == ConditionType.Not)
                    return !direct;
            }

            //Second parameter
            object bValue;
            if (B is IValuable vB)
                bValue = vB.GetValue();
            else
                bValue = B;

            //Two boolean values
            if (aValue is bool a && bValue is bool b)
                switch (Type)
                {
                    case ConditionType.Equals:
                        return a == b;
                    case ConditionType.NotEquals:
                        return a != b;
                    case ConditionType.Or:
                        return a || b;
                    case ConditionType.And:
                        return a && b;
                    case ConditionType.Not:
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            else if (aValue.IsNumericType(out dynamic aN) && bValue.IsNumericType(out dynamic bN))
            {
                switch (Type)
                {
                    case ConditionType.Greater:
                        return aN > bN;
                    case ConditionType.GreaterEquals:
                        return aN >= bN;
                    case ConditionType.Smaller:
                        return aN < bN;
                    case ConditionType.SmallerEquals:
                        return aN <= bN;
                    case ConditionType.Equals:
                        return aN == bN;
                    case ConditionType.NotEquals:
                        return aN != bN;
                    default:
                        throw new InvalidOperationException();
                }
            }
            else
            {
                if (Type == ConditionType.Equals)
                    return aValue == bValue;
                if (Type == ConditionType.NotEquals)
                    return aValue != bValue;
            }
            throw new InvalidOperationException();
        } 
    }
}
