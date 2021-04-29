using NCalc;
using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("assignment")]
    public class Assignment : IExpression
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public MemberCall Target { get; set; }

        [ParsedProperty(Index = 1, Type = BlockType.Node)]
        public Statement Value { get; set; } 

        public void Execute(Scope s)
        {
            string targetID = Target.ProduceVarIdentifier();
            s.SetMember(targetID, Value.GetValue(s));
        }
    }
}
