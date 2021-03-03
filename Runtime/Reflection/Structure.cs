using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    /// <summary>
    /// The <see cref="Structure"/> type implements the concept of a Context Structure. It is parsed from the struct grammar element
    /// </summary>
    public class Structure
    {
        public string Name { get; }
        public string Package { get; }
        public PropertyList Properties { get; }
    }
}
