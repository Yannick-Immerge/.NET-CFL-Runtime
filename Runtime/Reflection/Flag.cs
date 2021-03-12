using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("flag")]
    public class Flag
    {
        [ParsedProperty(Index = 2, Type = BlockType.Token)]
        public string Name { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Token)]
        public string DictName { get; set; }

        public FlagDictionary Dictionary { get; set; }
    }
}
