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
    public class Structure : IComponent
    {
        [ParsedProperty(Index = 0, Type = BlockType.Token)]
        public string Name { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public PropertyList Properties { get; set; }

        public IMember Parent { get; set; }

        public object GetValue(params object[] args)
        {
            throw new NotImplementedException();
        }

        public StructureObject Instantiate()
        {

        }
    }
}
