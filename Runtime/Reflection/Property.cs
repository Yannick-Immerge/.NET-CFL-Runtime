using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("prop")]
    public class Property : IMember, IAssignable
    {
        [ParsedProperty(Index = 0, Type = BlockType.Token)]
        public string Name { get; private set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public FlagCollection Flags { get; private set; }

        public IMember Parent { get; set; }
        public object Value { get; private set; }

        public void AssignValue(object value)
        {
            Value = value;
        }

        public object GetValue(Scope s)
            => Value;

        public object GetValue()
            => Value;
    }
}
