using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public class AbstractSyntaxNode
    {
        public Token[] Tokens { get; protected set; }
        public AbstractSyntaxNode[] Children { get; protected set; }
        public GrammarScheme Scheme { get; protected set; }
        public int Overload { get; protected set; }
        public AbstractSyntaxNode Parent { get; protected set; }
        public GrammarEntry Entry { get => Scheme.Overloads[Overload]; }

        protected AbstractSyntaxNode() { }

        public AbstractSyntaxNode(AbstractSyntaxNode parent, GrammarScheme scheme, TokenCollection tokens, GrammarSchemeCollection grammar, ref int index, Stack<int> history)
        {
            //Temporarily store in lists
            List<Token> tl = new List<Token>();
            List<AbstractSyntaxNode> cl = new List<AbstractSyntaxNode>();

            //Make choice
            Parent = parent;
            Scheme = scheme;
            Overload = history.Pop();

            //Create Node from parameters
            foreach(string s in Entry.Blocks)
            {
                if (s.StartsWith('!'))
                {
                    //Assume Grammar
                    AbstractSyntaxNode n = new AbstractSyntaxNode(this, grammar.GetNamed(s.Substring(1)), tokens, grammar, ref index, history);
                    cl.Add(n);
                }
                else
                {
                    //Assume Token
                    Token t = tokens[index++];
                    tl.Add(t);
                }
            }

            Tokens = tl.ToArray();
            Children = cl.ToArray();
        }

        protected void WriteToString(int shift, ref string s)
        {
            //Write self
            for(int i = 0; i < shift * 2; i++)
                s += "-";
            s += $"{Scheme.Name}\n";

            //Write Children
            foreach (AbstractSyntaxNode c in Children)
                c.WriteToString(shift + 1, ref s);
        }
    }
}
