using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation
{
    public struct TokenScheme
    {
        public string RegEx { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
