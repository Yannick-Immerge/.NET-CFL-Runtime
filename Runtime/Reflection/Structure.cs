using Runtime.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    /// <summary>
    /// The <see cref="Structure"/> type implements the concept of a Context Structure. It is parsed from the struct grammar element
    /// </summary>
    [ParsedComponent("struct")]
    [RegisteredComponent(ComponentType.Structure)]
    public class Structure : IComponent
    {
        [ParsedProperty(Index = 1, Type = BlockType.Token)]
        public string Name { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public PropertyList Properties { get; set; }

        public IMember Parent { get; set; }

        public object GetValue(Scope s)
        {
            throw new NotImplementedException();
        }

        public StructureObject Instantiate()
        {
            return null;
        }
    }
}
