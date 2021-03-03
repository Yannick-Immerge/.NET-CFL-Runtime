using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    public class Property : IMember
    {
        public IMember Parent { get; }
        public string Name { get; }
        public object Value { get; }
        public FlagCollection Flags { get; }


        public object GetValue(params object[] args)
            => Value;
    }
}
