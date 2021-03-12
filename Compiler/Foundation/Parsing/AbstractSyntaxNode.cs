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
        public bool IsBrokenDown { get; protected set; }

        protected AbstractSyntaxNode() 
        {
            IsBrokenDown = false;
        }

        public AbstractSyntaxNode(AbstractSyntaxNode parent, GrammarScheme scheme, TokenCollection tokens, GrammarSchemeCollection grammar, ref int index, Stack<int> history)
            => CreateNode(parent, scheme, tokens, grammar, ref index, history);

        public void BreakDownLists()
        {
            //Break down self
            if(Scheme.Overloads.Length == 2 && Scheme.Overloads[0].Blocks.Length == 2 && Scheme.Overloads[1].Blocks.Length == 1)
            {
                if (Scheme.Overloads[0].Blocks[0] == Scheme.Overloads[1].Blocks[0] && Scheme.Overloads[0].Blocks[1].Substring(1) == Scheme.Name)
                    BreakDownSelfList();
            }

            //Break down children
            foreach (AbstractSyntaxNode n in Children)
                n.BreakDownLists();
        }

        private void BreakDownSelfList()
        {
            List<AbstractSyntaxNode> n = new List<AbstractSyntaxNode>();
            AbstractSyntaxNode current = this;
            while(current.Overload == 0)
            {
                n.Add(current.Children[0]);
                current = current.Children[1];
            }
            n.Add(current.Children[0]);
            Children = n.ToArray();
            IsBrokenDown = true;
        }

        protected void CreateNode(AbstractSyntaxNode parent, GrammarScheme scheme, TokenCollection tokens, GrammarSchemeCollection grammar, ref int index, Stack<int> history)
        {
            //Temporarily store in lists
            List<Token> tl = new List<Token>();
            List<AbstractSyntaxNode> cl = new List<AbstractSyntaxNode>();

            //Make choice
            Parent = parent;
            Scheme = scheme;
            Overload = history.Pop();

            //Create Node from parameters
            foreach (string s in Entry.Blocks)
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
            s += $"{Scheme.Name}{(IsBrokenDown ? " [List]" : "")}\n";

            //Write Children
            foreach (AbstractSyntaxNode c in Children)
                c.WriteToString(shift + 1, ref s);
        }
    }
}
