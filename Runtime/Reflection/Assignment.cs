using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    public class Assignment
    {
        public Member Target { get; }
        public Member Value { get; }
        public object Constant { get; }
        public bool IsConstant { get; }
    }
}
