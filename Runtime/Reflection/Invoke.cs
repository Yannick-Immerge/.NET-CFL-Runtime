using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public class Invoke : IValuable, IExpression
    {
        public Progression Target { get; }
        public object[] Parameters { get; }

        public void Execute()
            => Target.GetValue(Parameters);

        public object GetValue()
            => Target.GetValue(Parameters);
    }
}
