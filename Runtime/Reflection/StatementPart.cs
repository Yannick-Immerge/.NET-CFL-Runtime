using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("statement-part")]
    public class StatementPart
    {
        [ParsedProperty(Index = 0, Type= BlockType.Token, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public string UnaryOperator { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public StatementPart Reference { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 1, Options = BlockOptions.SpecificOverload)]
        public Statement Embraced { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 2, Options = BlockOptions.SpecificOverload)]
        public MemberCall Member { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Overload = 3, Options = BlockOptions.SpecificOverload)]
        public Literal Literal { get; set; }

        public object GetValue(Scope s)
        {
            if (UnaryOperator != null)
                return Operations.Operate(UnaryOperator, Reference.GetValue(s));
            if (Embraced != null)
                return Embraced.GetValue(s);
            if (Member != null)
                return s.RetrieveMember(Member.ProduceVarIdentifier());
            if (Literal != null)
                return double.Parse(Literal.Value);

            return null;
        }

        public string GetExpressionString(Scope s)
        {
            if (UnaryOperator != null)
                return $"{UnaryOperator}{Reference.GetExpressionString(s)}";
            if (Embraced != null)
                return $"({Embraced.GetExpressionString(s)})";
            if (Member != null)
                return s.RetrieveMember(Member.ProduceVarIdentifier()).ToString();
            if (Literal != null)
                return Literal.Value;

            return null;
        }

    }
}
