using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("if-block")]
    public class IfBlock : IExpression
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public Condition Condition { get; }

        [ParsedProperty(Index = 1, Type = BlockType.Node)]
        public ExpressionList IfBody { get; }
        
        [ParsedProperty(Index = 2, Type = BlockType.Node)]
        public ExpressionList ElseBody { get; }

        public void Execute()
        {
            if ((bool)Condition.GetValue())
                IfBody.Execute();
            else
                ElseBody.Execute();
        }
    }
}
