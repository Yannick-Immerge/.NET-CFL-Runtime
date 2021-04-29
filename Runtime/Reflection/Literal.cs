using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("literal")]
    public class Literal
    {
        [ParsedProperty(Index =0, Type = BlockType.Token)]
        public string Value { get; set; }
    }
}
