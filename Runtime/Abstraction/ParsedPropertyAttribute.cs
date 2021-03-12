using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Foundation.Parsing;

namespace Runtime.Abstraction
{
    /// <summary>
    /// Parses a property from an element of an <see cref="AbstractSyntaxNode"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ParsedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the value indicating which specific block of the given <see cref="Type"/> should be used.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the value indicating wheter the property is parsed from a child node or one specific Token.
        /// </summary>
        public BlockType Type { get; set; }

        /// <summary>
        /// Gets or sets further options specifying how a property should be parsed.
        /// </summary>
        public BlockOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the overload this attribute applies to when <see cref="BlockOptions.SpecificOverload"/> is enabled.
        /// </summary>
        public int Overload { get; set; }

        public ParsedPropertyAttribute()
            => Options = BlockOptions.None;
    }

    public enum BlockType
    {
        Node,
        Token
    }

    public enum BlockOptions : int
    {
        SpecificOverload = 4,
        DirectUnfold = 2,
        None = 0
    }
}
