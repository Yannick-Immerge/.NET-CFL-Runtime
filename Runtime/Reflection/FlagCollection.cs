using Compiler.Foundation.Parsing;
using Runtime.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("flag-list", ConstructorParsing = true)]
    public class FlagCollection : ParsedEnumerable<Flag>
    {
        public FlagCollection(AbstractSyntaxNode node, ProgramBuilder context) : base(node, context) { }
    }
}
