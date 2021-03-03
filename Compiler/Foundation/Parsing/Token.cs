using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public struct Token
    {
        public static readonly Token NullToken = default(Token);

        public TokenScheme Scheme { get; set; }
        public string Value { get; set; }
        public string Name { get => Scheme.Name; }
    }
}
