using Runtime.Abstraction;
using Runtime.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("invoke")]
    public class Invoke : IExpression
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public MemberCall Target { get; }

        [ParsedProperty(Index = 1, Type = BlockType.Node)]
        public StatementCollection Parameters { get; }

        public void Execute(Scope s)
        {
            Progression p = s.Runtime.GetRegisteredProgression(Target.ProduceVarIdentifier());

            //Match params
            Parameter[] ps = p.Parameters.ToArray();
            Statement[] ss = Parameters.ToArray();
            for (int i = 0; i < ps.Length; i++)
                s.SetValue(ps[i].Name, ss[i].GetValue(s));
            
            p.GetValue(s);
        }
    }
}
