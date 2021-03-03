using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Foundation
{
    /// <summary>
    /// Stores a number of <see cref="TokenScheme"/> objects ordered by their priority.
    /// </summary>
    public class TokenSchemeCollection : ICollection<TokenScheme>
    {
        private List<TokenScheme> _schemes;

        public int Count => _schemes.Count;

        public bool IsReadOnly => false;

        public TokenSchemeCollection()
        {
            _schemes = new List<TokenScheme>();
        }

        public int ReadFromStream(Stream s)
        {
            StreamReader read = new StreamReader(s);

            int op = _schemes.Count, p = op;

            while (!read.EndOfStream)
            {
                string line = read.ReadLine();

                //Ignore Comment and empty Lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                bool isC = false;
                foreach (char c in line)
                    if (c == '#')
                    {
                        isC = true;
                        break;
                    }
                    else if (!char.IsWhiteSpace(c))
                        break;
                if (isC)
                    continue;

                //Parse with RegEx
                Match m = Regex.Match(line, @"([a-zA-Z-]+)\s*~\s*(.+)");
                if (!m.Success)
                    continue;

                //Create and add Token
                TokenScheme t = new TokenScheme() { Name = m.Groups[1].Value, RegEx = m.Groups[2].Value, Priority = -(p++) };
                Add(t);
            }

            //Return number of added Tokens
            return p - op;
        }

        public void Add(TokenScheme item)
        {
            bool ins = false;
            for(int i = 0; i < _schemes.Count; i++)
            {
                //Check priority
                if (_schemes[i].Priority < item.Priority) 
                {
                    _schemes.Insert(i, item);
                    ins = true;
                    break;
                }
            }
            if (!ins)
                _schemes.Add(item);
        }

        public void Clear()
        {
            _schemes.Clear();
        }

        public bool Contains(TokenScheme item)
        {
            return _schemes.Contains(item);
        }

        public void CopyTo(TokenScheme[] array, int arrayIndex)
        {
            _schemes.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TokenScheme> GetEnumerator()
        {
            return _schemes.GetEnumerator();
        }

        public bool Remove(TokenScheme item)
        {
            return _schemes.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
