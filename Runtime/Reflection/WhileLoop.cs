using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("while-block")]
    public class WhileBlock : IExpression
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public Condition Condition { get; set; }

        [ParsedProperty(Index = 1, Type = BlockType.Node)]
        public ExpressionList Body { get; set; }

        public void Execute(Scope s)
        {
            while ((bool)Condition.GetValue(s))
                Body.Execute(s);
        }
    }
}
