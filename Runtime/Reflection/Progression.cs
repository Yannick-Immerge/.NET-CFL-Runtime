using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("prog")]
    [RegisteredComponent(ComponentType.Progression)]
    public class Progression : IComponent
    {
        [ParsedProperty(Index = 1, Type = BlockType.Token)]
        public string Name { get; set; }

        [ParsedProperty(Index = 1, Type = BlockType.Node)]
        public ExpressionList Body { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public ParameterCollection Parameters { get; set; }

        public PrimitiveType? ReturnType { get; }
        public FlagCollection ReturnFlags { get; }
        
        public IMember Parent { get; set; }

        public object GetValue(Scope s)
        {
            //Execute with given scope
            foreach (IExpression i in Body)
                i.Execute(s);

            if (s.ContainsVariable("%RET%"))
                return s.GetValue("%RET%", out _);
            return null;
        }

    }
}
