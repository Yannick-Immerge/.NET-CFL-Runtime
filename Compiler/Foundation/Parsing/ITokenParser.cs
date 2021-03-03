using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public interface ITokenParser
    {
        public TokenSchemeCollection BaseTokens { get; set; }

        public abstract TokenCollection ParseTokensFromStream(Stream s);
        public abstract bool IsCompatibleWithSpecs(string specs);
    }
}
