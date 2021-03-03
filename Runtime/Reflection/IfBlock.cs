using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    public class IfBlock : Expression
    {
        public Condition Condition { get; }
        public ExpressionList IfBody { get; }
        public ExpressionList ElseBody { get; }
    }
}
