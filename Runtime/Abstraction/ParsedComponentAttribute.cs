using Compiler.Foundation.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Abstraction
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ParsedComponentAttribute : Attribute
    {
        public string NodeName { get; }
        public bool IsOwnChild { get; set; }
        public bool ConstructorParsing { get; set; }

        public ParsedComponentAttribute(string name)
        { 
            NodeName = name;
            IsOwnChild = false;
            ConstructorParsing = false;
        }
    }
}
