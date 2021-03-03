using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    /// <summary>
    /// A node of an <see cref="SyntaxParsingTree"/> that can be unfolded given a certain <see cref="TokenCollection"/>.
    /// </summary>
    public class SyntaxParsingNode
    {
        public GrammarScheme Scheme { get; set; }
        public int Overload { get; set; }
        public TokenSpan Span { get; set; }

        public SyntaxParsingNode Parent { get; protected set; }
        public TokenCollection Collection { get; protected set; }

        public GrammarEntry Entry { get => Scheme.Overloads[Overload]; }

        public SyntaxParsingNode ProduceChild()
            => new SyntaxParsingNode() { Parent = this, Collection = Collection };

        public Token GetRelativeToken(int i)
            => Collection[i + Span.StartToken];
    }
}
