using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public enum ConditionType
    {
        Greater,
        GreaterEquals,
        Smaller,
        SmallerEquals,
        Equals,
        NotEquals,
        Or,
        And,
        Not,
        Direct
    }

    public enum PrimitiveType
    {
        Integer,
        Byte,
        Double,
        Boolean,
        Character,
        Array
    }
}
