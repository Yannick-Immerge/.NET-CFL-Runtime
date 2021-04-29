using Compiler.Foundation.Parsing;
using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    [ParsedComponent("statement-list", ConstructorParsing = true)]
    public class StatementCollection : ParsedEnumerable<Statement>
    {
        public StatementCollection(AbstractSyntaxNode node, ProgramBuilder context) : base(node, context) { }   
    }
}
