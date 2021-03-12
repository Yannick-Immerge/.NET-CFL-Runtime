using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    [ParsedComponent("container")]
    public class Container : IMember
    {
        [ParsedProperty(Index = 0, Type = BlockType.Node, Options = BlockOptions.DirectUnfold)]
        public IEnumerable<IComponent> Components { get; set; }
        
        [ParsedProperty(Index = 0, Type = BlockType.Token)]
        public string Name { get; set; }
        
        public IMember Parent { get => null; }

        public object GetValue(params object[] args)
            => throw new NotImplementedException();
    }
}
