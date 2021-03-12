using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    /// <summary>
    /// Specifies a stack and heap (defined in the <see cref="CFLRuntime"/>) for usage in a specific area of code execution.
    /// </summary>
    public class Scope
    {
        public CFLRuntime Runtime { get; }

        private Dictionary<string, object> _stack;

        public Scope(CFLRuntime runtime)
        {

        }
    }
}
