using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public class VariableInfo
    {
        public VariableInfo(string name, Scope s, bool fix)
        { 
            Name = name;
            Container = s;
            FixedType = fix;
        }

        public string Name { get; }
        public Scope Container { get; }
        public bool FixedType { get; }
        public CFLType Type { get => _type; internal set => SetType(value); }

        private CFLType _type;

        private void SetType(CFLType type)
        {
            if (type == Type)
                return;

            if (FixedType && Type != null)
                throw new InvalidOperationException("Cannot change type of fixed type variable.");

            _type = type;
        }
    }
}
