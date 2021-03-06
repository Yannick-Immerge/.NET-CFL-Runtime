﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public class FlagDictionary : IEnumerable<Flag>, IMember
    {
        public int Count { get => _flags.Count; }
        public IMember Parent { get; set; }
        public string Name { get; }

        private Dictionary<string, Flag> _flags = new Dictionary<string, Flag>();

        public IEnumerator<Flag> GetEnumerator()
            => _flags.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public object GetValue(Scope s)
        {
            throw new NotImplementedException();
        }
    }
}
