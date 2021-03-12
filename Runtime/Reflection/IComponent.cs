using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("comp", IsOwnChild = true)]
    public interface IComponent : IMember
    {   
    }
}
