using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    public class Progression : IMember
    {
        public string Name { get; }
        public ExpressionList Body { get; }
        public ParameterList Parameters { get; }
        public IMember Parent { get => null; }

        public object GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
