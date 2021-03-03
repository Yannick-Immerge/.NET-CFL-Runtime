using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public class TokenCollection : ICollection<Token>
    {
        private List<Token> _tokens;

        public int Count => _tokens.Count;

        public bool IsReadOnly => false;

        public TokenCollection()
        {
            _tokens = new List<Token>();
        }

        public Token this[int i]
        {
            get => _tokens[i];
        }

        public void Add(Token item)
        {
            _tokens.Add(item);
        }

        public void Clear()
        {
            _tokens.Clear();
        }

        public bool Contains(Token item)
        {
            return _tokens.Contains(item);
        }

        public void CopyTo(Token[] array, int arrayIndex)
        {
            _tokens.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return ((IEnumerable<Token>)_tokens).GetEnumerator();
        }

        public bool Remove(Token item)
        {
            return _tokens.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_tokens).GetEnumerator();
        }

        public override string ToString()
        {
            string msg = "";
            foreach (Token t in this)
                msg += $"<{t.Value}>";

            return msg;
        }
    }
}
