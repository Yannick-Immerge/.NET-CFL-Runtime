using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation
{
    public struct GrammarScheme
    {
        public string Name { get; set; }
        public GrammarEntry[] Overloads { get; set; }
        public bool IsContainer { get; set; }
    }

    public struct GrammarEntry
    {
        public string[] Blocks { get; set; }
        public int Priority { get; set; }
    }
}
