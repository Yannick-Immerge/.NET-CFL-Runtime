using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Foundation
{
    public class GrammarSchemeCollection : ICollection<GrammarScheme>
    {
        private List<GrammarScheme> _schemes;

        public GrammarScheme Container { get => _schemes[_container]; }
        public int Count => _schemes.Count;
        public bool IsReadOnly => false;

        private int _container;

        public GrammarSchemeCollection()
        {
            _schemes = new List<GrammarScheme>();
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
                Match m = Regex.Match(line, @"(\*?)([a-zA-Z-]+)\s*~((?:\s*\|?(?:<!?[a-zA-Z-]+>)*)*)");
                if (!m.Success)
                    continue;

                //Initialize Scheme
                MatchCollection defs = Regex.Matches(m.Groups[3].Value, @"(?:<!?[a-zA-Z-]+>)+");
                GrammarEntry[] entries = new GrammarEntry[defs.Count];
                GrammarScheme scheme = new GrammarScheme() { Name = m.Groups[2].Value, Overloads = entries, IsContainer = m.Groups[1].Length > 0 };

                //Create Entries
                int i = 0;
                foreach(Match d in defs)
                {
                    //Create GrammarEntry
                    MatchCollection blocks = Regex.Matches(d.Value, @"(?:<(!?[a-zA-Z-]+)>)");
                    GrammarEntry e = new GrammarEntry { Priority = i, Blocks = new string[blocks.Count] };
                    for (int j = 0; j < blocks.Count; j++)
                        e.Blocks[j] = blocks[j].Groups[1].Value;
                    scheme.Overloads[i++] = e;
                }

                //Add Scheme
                Add(scheme);
            }

            //Return number of added Tokens
            return p - op;
        }

        public GrammarScheme GetNamed(string name)
        {
            foreach (GrammarScheme scheme in _schemes)
                if (scheme.Name == name)
                    return scheme;

            throw new ArgumentException("No scheme with given name has been found.");
        }

        public void Add(GrammarScheme item)
        {
            _schemes.Add(item);
        }

        public void Clear()
        {
            _schemes.Clear();
        }

        public bool Contains(GrammarScheme item)
        {
            return _schemes.Contains(item);
        }

        public void CopyTo(GrammarScheme[] array, int arrayIndex)
        {
            _schemes.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GrammarScheme> GetEnumerator()
        {
            return _schemes.GetEnumerator();
        }

        public bool Remove(GrammarScheme item)
        {
            return _schemes.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
