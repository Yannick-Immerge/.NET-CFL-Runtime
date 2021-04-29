using Runtime.Abstraction;
using Runtime.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("if-block")]
    public class IfBlock : IExpression
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public Condition Condition { get; set; }

        [ParsedProperty(Index = 1, Type = BlockType.Node)]
        public ExpressionList IfBody { get; set; }
        
        [ParsedProperty(Index = 2, Type = BlockType.Node, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public ExpressionList ElseBody { get; set; }

        public void Execute(Scope s)
        {
            if ((bool)Condition.GetValue(s))
                IfBody.Execute(s);
            else
                if(ElseBody != null)
                    ElseBody.Execute(s);
        }
    }
}
