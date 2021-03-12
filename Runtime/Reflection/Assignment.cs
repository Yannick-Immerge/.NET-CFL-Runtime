using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("assignment")]
    public class Assignment : IExpression
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public MemberCall Target { get; set; }
        [ParsedProperty(Index = 1, Type = BlockType.Node)]
        public MemberCall Value { get; set; } 

        public void Execute()
        {
            //Write Value to target
        }
    }
}
