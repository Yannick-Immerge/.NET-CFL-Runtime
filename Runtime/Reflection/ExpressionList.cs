using Compiler.Foundation.Parsing;
using Runtime.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("expr-list", ConstructorParsing = true)]
    public class ExpressionList : ParsedEnumerable<IExpression>, IExpression
    {
        public ExpressionList(AbstractSyntaxNode node, ProgramBuilder context) : base(node, context) 
        { 
        }

        public void Execute(Scope s)
        {
            foreach (IExpression i in this)
                i.Execute(s);
        }
    }
}
