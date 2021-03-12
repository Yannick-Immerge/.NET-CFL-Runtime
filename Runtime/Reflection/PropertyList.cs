using Compiler.Foundation.Parsing;
using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("prop-list", ConstructorParsing = true)]
    public class PropertyList : ParsedEnumerable<Property>
    {
        public PropertyList(AbstractSyntaxNode node, ProgramBuilder context) : base(node, context) { }

        public Property this[string name]
        {
            get
            {
                foreach (Property p in this)
                    if (p.Name == name)
                        return p;
                throw new ArgumentException();
            }
        }
    }
}
