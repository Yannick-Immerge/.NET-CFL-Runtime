using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    public class FlagCollection : IEnumerable<Flag>
    {
        public int Count { get => _flags.Count; }

        private List<Flag> _flags;

        public IEnumerator<Flag> GetEnumerator()
            => _flags.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
