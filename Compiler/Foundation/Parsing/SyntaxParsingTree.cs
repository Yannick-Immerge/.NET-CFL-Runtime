using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public class SyntaxParsingTree : SyntaxParsingNode
    {
        public SyntaxParsingTree(TokenCollection tokens)
        {
            Parent = null;
            Collection = tokens;
            Span = new TokenSpan(0, tokens.Count - 1);
            Overload = 0;
        }
    }
}
