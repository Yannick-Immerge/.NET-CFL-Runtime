using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Debug
{
    public class DebugScope : Scope
    {
        public DebugScope(CFLRuntime runtime, Scope parent) : base(runtime, parent)
        {

        }

        public override VariableInfo GenerateVariable(string name, PrimitiveType type)
        {
            Console.WriteLine($"Generated primtive Variable: {name}");
            return base.GenerateVariable(name, type);
        }

        public override VariableInfo GenerateVariable(string name, Structure type)
        {
            Console.WriteLine($"Generated reference Variable: {name}");
            return base.GenerateVariable(name, type);
        }

        public override void SetValue(string name, object value)
        {
            Console.WriteLine($"Set Value of {name} to: {value}");
            base.SetValue(name, value);
        }
    }
}
