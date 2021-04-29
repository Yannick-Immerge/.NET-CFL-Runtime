using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("statement")]
    public class Statement
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public StatementPart Value { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Token, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public string BinaryOperator { get; set; }

        [ParsedProperty(Index = 1, Type = BlockType.Node, Overload = 0, Options = BlockOptions.SpecificOverload)]
        public Statement Reference { get; set; }

        public object GetValue(Scope s)
        {
            //Manually execute operations
            if (BinaryOperator == null)
                return Value.GetValue(s);

            //Recursevely work on Statement tree
            Statement cur = this;
            List<object> parts = new List<object>();
            List<(string Op, int Prio)> operators = new List<(string, int)>();
            int maxP = int.MinValue;
            while (true)
            {
                parts.Add(cur.Value);

                if (cur.BinaryOperator == null)
                    break;

                int p = Operations.Priority(cur.BinaryOperator);
                if (p > maxP)
                    maxP = p;
                operators.Add((cur.BinaryOperator, p));
                cur = cur.Reference;
            }

            for(int p = maxP; p <= maxP; p--)
            {
                for(int i = 0; i < operators.Count; i++)
                {
                    if(operators[i].Prio == p)
                    {
                        object a = parts[i] is StatementPart sPa ? sPa.GetValue(s) : parts[i];
                        object b = parts[i + 1] is StatementPart sPb ? sPb.GetValue(s) : parts[i + 1];
                        object r = Operations.Operate(operators[i].Op, a, b);

                        parts[i] = r;
                        parts.RemoveAt(i + 1);
                        operators.RemoveAt(i);
                        i--;
                    }
                }

                if (operators.Count == 0)
                    break;
            }

            return parts[0];
        }

        public string GetExpressionString(Scope s)
        {
            if (BinaryOperator != null)
                return $"{Value.GetExpressionString(s)} {BinaryOperator} {Reference.GetExpressionString(s)}";
            return Value.GetExpressionString(s);
        }
    }
}
