using Compiler.Foundation.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Abstraction
{
    public delegate object ParseFromSyntaxTree(AbstractSyntaxNode node);

    public class ParsedComponentAttribute : Attribute
    {
    }
}
