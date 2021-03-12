using Compiler.Foundation.Parsing;
using Runtime.Abstraction;
using System.Collections;
using System.Collections.Generic;

namespace Runtime.Foundation
{
    [ParsedComponent("param-list", ConstructorParsing = true)]
    public class ParameterCollection : ParsedEnumerable<Parameter>
    {
        public ParameterCollection(AbstractSyntaxNode node, ProgramBuilder context) : base(node, context) { }

        public IEnumerable<Parameter> GetOrderedParameters(IEnumerable<string> names)
        {
            foreach (string n in names)
                foreach (Parameter p in _contents)
                    if (p.Name == n) {
                        yield return p;
                        break;
                    }
        }

    }
}