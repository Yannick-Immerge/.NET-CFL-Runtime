using Compiler.Foundation;
using Compiler.Foundation.Parsing;
using Compiler.Parsers;
using Compiler.Resources;
using Runtime.Abstraction;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.IO;

namespace Runtime
{
    public class Program
    {
        public static void Main(string[] args)
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

            //Parse to abstract
            CFLRuntime rt = new CFLRuntime();
            ProgramBuilder builder = new ProgramBuilder(rt);
            Container cnt = builder.BuildFromTree(ast);

            //Console.WriteLine("Calculate sum of n squares with CFL [Enter integer]:");
            //object n = Console.ReadLine().EvaluateMathExpression();
            //Console.WriteLine(rt.Execute("sum_n_squares", ("n", n)));

            Console.WriteLine("Count number of prime numbers below n with CFL [Enter integer n]:");
            object n = Console.ReadLine().EvaluateMathExpression();
            Console.WriteLine(rt.Execute("primes_below_n", ("n", n)));
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
