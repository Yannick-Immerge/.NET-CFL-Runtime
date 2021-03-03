using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public interface IProgram
    {
        public string Name { get; }
        public IEnumerable<ICommand> GetCommands();
    }
}
