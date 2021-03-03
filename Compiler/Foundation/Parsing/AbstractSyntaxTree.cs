using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public class AbstractSyntaxTree : AbstractSyntaxNode
    {
        public GrammarSchemeCollection Grammar { get; private set; }
        
        public AbstractSyntaxTree()
        {
            Parent = null;
            Children = null;
            Tokens = null;
        }

        public void CreateTree(GrammarSchemeCollection grammar, TokenCollection tokens, Stack<int> history)
        {
            Grammar = grammar;

            if (Grammar == null)
                throw new InvalidOperationException("A grammar has to be assigned.");

            Scheme = Grammar.Container;
            Overload = 0;
            Tokens = new Token[0];
            int index = 0;
            Children = new AbstractSyntaxNode[] { new AbstractSyntaxNode(this, Grammar.GetNamed(Entry.Blocks[0].Substring(1)), tokens, Grammar, ref index, history) };
        }

        public override string ToString()
        {
            string s = "";
            WriteToString(0, ref s);

            return s;
        }
    }
}
