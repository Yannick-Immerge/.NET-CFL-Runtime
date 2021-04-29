using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    /// <summary>
    /// Defines an interface for member types. Members can be any objects contained within another object.
    /// </summary>
    public interface IMember
    {
        public IMember Parent { get; }
        public string Name { get; }
        public object GetValue(Scope scope);
    }
}
