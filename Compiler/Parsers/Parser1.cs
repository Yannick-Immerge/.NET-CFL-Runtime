using Compiler.Foundation;
using Compiler.Foundation.Parsing;
using Compiler.Foundation.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Parsers
{
    public class Parser1 : ITokenParser, IAbstractSyntaxParser
    {
        public TokenSchemeCollection BaseTokens { get; set; }
        public GrammarSchemeCollection BaseGrammar { get; set; }

        bool ITokenParser.IsCompatibleWithSpecs(string specs)
            => true;
        bool IAbstractSyntaxParser.IsCompatibleWithSpecs(string specs)
            => true;

        public Stack<int> ParseHistoryFromStream(Stream s)
            => ParseHistory(ParseTokensFromStream(s));

        public TokenCollection ParseTokensFromStream(Stream s)
        {
            //Read entire stream
            StreamReader read = new StreamReader(s);
            string value = read.ReadToEnd();

            //Replace with string subs
            int i = value.Length -1;
            int j = -1;
            bool open = false;
            int o = -1;
            List<string> org = new List<string>();

            //Iterate backwards
            while(i > -1 && (j = value.LastIndexOf('"', i)) != -1)
            {
                //Decide if literal is valid
                if (j > 0 && value[j - 1] == '\\')
                {
                    i = j - 1;
                    continue;
                }

                //Open or close 
                if (open)
                {
                    org.Add(value.Substring(j, o - j + 1));
                    value = value.Remove(j, o - j + 1).Insert(j, $"%STR{org.Count - 1}%");
                    open = false;
                }
                else
                {
                    o = j;
                    open = true;
                }

                i = j - 1;
            }

            //Tokenize
            TokenCollection c = new TokenCollection();
            while(value != "")
                c.Add(ParseNext(ref value));

            return c;
        }
        public Stack<int> ParseHistory(TokenCollection tokens)
        {
            //Create Tree Root
            SyntaxParsingTree tree = new SyntaxParsingTree(tokens);
            tree.Scheme = BaseGrammar.Container;


            //Produce Roots recursively
            DebugStack history = new DebugStack();
            bool worked = CreateHistoryRecursevely(history, tree);

            return (Stack<int>)history;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="history"></param>
        /// <param name="node"></param>
        /// <returns>Number of false pushes to history</returns>
        private bool CreateHistoryRecursevely(DebugStack history, SyntaxParsingNode node)
        {
            //Look at GrammarEntry
            GrammarEntry current = node.Entry;

            //Try to match Tokens or Entries store original history
            int h = history.Count;
            int t = node.Span.StartToken;
            bool lastToken = false;
            for(int k = 0; k < current.Blocks.Length; k++)
            {
                string b = current.Blocks[k];
                //Look at next token if last one was
                if (lastToken)
                {
                    t++;
                    lastToken = false;
                }

                //Consider Grammar Block
                if (b.StartsWith('!'))
                {
                    bool found = false;
                    GrammarScheme next = BaseGrammar.GetNamed(b.Substring(1));
                    for (int i = 0; i < next.Overloads.Length; i++)
                    {
                        //Store Route
                        history.Push(i);

                        //Create Child and continue
                        SyntaxParsingNode c = node.ProduceChild();
                        c.Span = new TokenSpan(t);
                        c.Overload = i;
                        c.Scheme = next;
                        if (CreateHistoryRecursevely(history, c))
                        {
                            t = c.Span.EndToken;
                            lastToken = true;
                            found = true;
                            break;
                        }
                        else
                            history.Pop();
                    }

                    if (!found)
                    {
                        Console.WriteLine($"Considered Element: {node.Scheme.Name} [{node.Overload}]");
                        Console.WriteLine($" - Could not be created because of block: {b}");
                        Console.WriteLine($" - Error: No fitting overload found.\n---");
                        //Reset History
                        while (h < history.Count)
                            history.Pop();

                        //Return
                        return false;
                    }
                }
                //Consider Token Block
                else
                {
                    //Match Token
                    if (t < node.Collection.Count && b == node.Collection[t].Name)
                        lastToken = true;
                    else
                    {
                        Console.WriteLine($"Considered Element: {node.Scheme.Name} [{node.Overload}]");
                        Console.WriteLine($" - Could not be created because of block: {b}");
                        Console.WriteLine($" - Error: { (t < node.Collection.Count ? $"Token did not match {node.Collection[t].Name}" : "The definition exceeds the assigned tokens.") }.\n---");
                        //Reset History
                        while (history.Count > h)
                            history.Pop();

                        //Return
                        return false;
                    }
                }
            }

            Console.WriteLine($"Considered Element: {node.Scheme.Name} [{node.Overload}]");
            Console.WriteLine($" - Succesfully created!");
            Console.WriteLine($" - Token Span: {node.Span.StartToken} - {t}\n---");

            //Node was a match
            node.Span.EndToken = t;
            return true;
        }

        private Token ParseNext(ref string act)
        {
            //Remove leading whitespace
            while (char.IsWhiteSpace(act[0]))
                act = act.Remove(0, 1);

            (int, int, Token) min = (int.MaxValue, 0, Token.NullToken);
            foreach(TokenScheme s in BaseTokens)
            {
                //Match next TokenScheme
                Match m = Regex.Match(act, s.RegEx);
                if (!m.Success)
                    continue;
                if (m.Index < min.Item1)
                    min = (m.Index, m.Length, new Token() { Scheme = s, Value = m.Value });
                else if (m.Index == min.Item1 && m.Length > min.Item2)
                    min = (m.Index, m.Length, new Token() { Scheme = s, Value = m.Value });
            }
            int size = min.Item3.Value.Length;
            act = size < act.Length ? act.Substring(size) : "";
            return min.Item3;
        }
    }
}
