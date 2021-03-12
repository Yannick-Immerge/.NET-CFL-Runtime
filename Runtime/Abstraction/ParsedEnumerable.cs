using Compiler.Foundation.Parsing;
using Runtime.Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Abstraction
{
    public class ParsedEnumerable<T> : IEnumerable<T>
    {
        protected List<T> _contents;

        public ParsedEnumerable(AbstractSyntaxNode node, ProgramBuilder context)
        {
            _contents = new List<T>();
            foreach (AbstractSyntaxNode c in node.Children)
                _contents.Add((T)context.BuildNode(c));
        }

        public IEnumerator<T> GetEnumerator()
            => _contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
