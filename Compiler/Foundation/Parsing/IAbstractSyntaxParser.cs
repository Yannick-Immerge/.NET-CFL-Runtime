using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public interface IAbstractSyntaxParser
    {
        public GrammarSchemeCollection BaseGrammar { get; set; }

        public Stack<int> ParseHistory(TokenCollection tokens);
        public bool IsCompatibleWithSpecs(string specs);
    }
}
