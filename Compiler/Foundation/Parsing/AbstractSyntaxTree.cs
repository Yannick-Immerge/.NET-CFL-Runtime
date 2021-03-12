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
            if (grammar == null)
                throw new InvalidOperationException("A grammar has to be assigned.");

            //Create node
            int index = 0;
            CreateNode(null, grammar.Container, tokens, grammar, ref index, history);
        }

        public override string ToString()
        {
            string s = "";
            WriteToString(0, ref s);

            return s;
        }
    }
}
