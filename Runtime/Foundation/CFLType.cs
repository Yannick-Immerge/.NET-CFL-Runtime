using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public class CFLType
    {
        public string Name { get; }
        public object TypeDefinition { get => IsPrimitive ? (object)_type : (object)_struct; }
        public bool IsPrimitive { get; }
        public Structure StructureType { get => TypeDefinition as Structure; }
        public PrimitiveType? PrimitiveType { get => TypeDefinition as PrimitiveType?; }

        private PrimitiveType _type;
        private Structure _struct;

        internal CFLType(string name, object typedef)
        {
            Name = name;
            if (typedef is Structure s)
            {
                _struct = s;
                IsPrimitive = false;
            }
            else if (typedef is PrimitiveType p)
            {
                _type = p;
                IsPrimitive = true;
            }
        }

        public static implicit operator CFLType(Structure s)
        {
            if (!CFLRuntime.CurrentRuntime.ContainsType(s.Name))
                CFLRuntime.CurrentRuntime.RegisterType(s.Name, s);

            return CFLRuntime.CurrentRuntime.GetRegisteredType(s.Name);
        }

        public static implicit operator CFLType(PrimitiveType t)
        {
            string name = t.ToString();
            if (!CFLRuntime.CurrentRuntime.ContainsType(name))
                CFLRuntime.CurrentRuntime.RegisterType(name, t);

            return CFLRuntime.CurrentRuntime.GetRegisteredType(name);
        }
    }
}
