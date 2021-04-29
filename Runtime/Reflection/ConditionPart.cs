using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("condition-part")]
    public class ConditionPart
    {
        [ParsedProperty(Index = 0, Type= BlockType.Token, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public string UnaryOperator { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public ConditionPart Reference { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 1, Options = BlockOptions.SpecificOverload)]
        public Condition Embraced { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 2, Options = BlockOptions.SpecificOverload)]
        public BooleanLiteral BLiteral { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 3, Options = BlockOptions.SpecificOverload)]
        public Statement Statement { get; set; }

        public string GetExpressionString(Scope s)
        {
            if (UnaryOperator != null)
                return $"{UnaryOperator}{Reference.GetExpressionString(s)}";
            if (Embraced != null)
                return $"({Embraced.GetExpressionString(s)})";
            if (BLiteral != null)
                return BLiteral.Value;
            if (Statement != null)
                return Statement.GetValue(s).ToString();

            return null;
        }

    }
}
