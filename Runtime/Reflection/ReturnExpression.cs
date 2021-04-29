using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("return")]
    public class ReturnExpression : IExpression
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public Statement Statement { get; set; }

        public void Execute(Scope s)
        {
            s.SetValue("%RET%", Statement.GetValue(s));
        }
    }
}
