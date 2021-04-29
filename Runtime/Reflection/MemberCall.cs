using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("member")]
    public class MemberCall
    {
        [ParsedProperty(Index = 0, Type = BlockType.Token)]
        public string LastName { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node, Options = BlockOptions.SpecificOverload, Overload = 0)]
        public MemberCall Body { get; set; }

        public string ProduceVarIdentifier()
            => LastName += (Body == null) ? "" : "." + Body.ProduceVarIdentifier();
    }
}
