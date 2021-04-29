using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("condition")]
    public class Condition
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public ConditionPart Value { get; set; }

        [ParsedProperty(Index = 1, Type = BlockType.Node, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public BooleanOperator BinaryOperator { get; set; }

        [ParsedProperty(Index = 2, Type = BlockType.Node, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public Condition Reference { get; set; }

        public object GetValue(Scope s)
        {
            //Create expression string
            string expr = GetExpressionString(s);

            //Return value
            return expr.EvaluateBoolExpression();
        }

        public string GetExpressionString(Scope s)
        {
            if (BinaryOperator != null)
                return $"{Value.GetExpressionString(s)} {BinaryOperator.Value} {Reference.GetExpressionString(s)}";
            return Value.GetExpressionString(s);
        }
    }
}
