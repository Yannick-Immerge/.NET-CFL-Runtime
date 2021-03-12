using Runtime.Abstraction;

namespace Runtime.Foundation
{
    [ParsedComponent("param")]
    public class Parameter
    {
        [ParsedProperty(Index = 0, Type = BlockType.Token)]
        public string Name { get; set; }

        [ParsedProperty(Index = 0, Type = BlockType.Node)]
        public FlagCollection Flags { get; set; }
    }
}