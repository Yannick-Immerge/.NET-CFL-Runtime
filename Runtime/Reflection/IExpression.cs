using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("expr", IsOwnChild = true)]
    public interface IExpression
    {
        public void Execute(Scope s);
    }
}
