using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Parsing
{
    public class TokenSpan
    {
        public int StartToken { get; set; }
        public int EndToken { get; set; }
        public int Size { get => EndToken == -1 ? throw new InvalidOperationException("The end of the span has not yet been defined.") : EndToken - StartToken + 1; }

        public TokenSpan(int start, int end)
        {
            StartToken = start;
            EndToken = end;
        }

        public TokenSpan(int start)
        {
            StartToken = start;
            EndToken = -1;
        }
    }
}
