using Compiler.Foundation;
using Compiler.Foundation.Parsing;
using Compiler.Parsers;
using Compiler.Resources;
using System;
using System.Collections.Generic;
using System.IO;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load specs
            (FileInfo, FileInfo)? s = SpecificationsManager.GetSpecification("test-002");
            FileStream ts = s.Value.Item1.OpenRead();
            FileStream gs = s.Value.Item2.OpenRead();

            //Parse tokens
            TokenSchemeCollection t = new TokenSchemeCollection();
            t.ReadFromStream(ts);

            //Parse grammar
            GrammarSchemeCollection g = new GrammarSchemeCollection();
            g.ReadFromStream(gs);

            //Load test
            FileInfo test = TestsManager.GetTest("test_ast");
            FileStream testStream = test.OpenRead();

            //Parse test File
            Parser1 p1 = new Parser1() { BaseTokens = t, BaseGrammar = g };
            TokenCollection tokens = p1.ParseTokensFromStream(testStream);
            Stack<int> history = p1.ParseHistory(tokens);
            AbstractSyntaxTree ast = new AbstractSyntaxTree();
            ast.CreateTree(g, tokens, ReverseHistory(history));

            Console.WriteLine(ast);
            ast.BreakDownLists();
            Console.WriteLine(ast);
        }
        private static Stack<int> ReverseHistory(Stack<int> h)
        {
            Stack<int> ret = new Stack<int>();
            while (h.Count > 0)
                ret.Push(h.Pop());

            return ret;
        }
    }
}
