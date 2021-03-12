using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public class StructureObject
    {
        public Structure Type { get; }

        private Dictionary<Property, object> _values;

        public StructureObject(Structure type)
        {
            Type = type;

            //Create Properties and assign default values
            _values = new Dictionary<Property, object>();
            foreach (Property p in type.Properties)
            {
                _values.Add(p, null);
            }
        }

        public object GetProperty(string name)
        {
            foreach (Property p in _values.Keys)
                if (p.Name == name)
                    return _values[p];

            throw new ArgumentException("For this object no property with given name is defined.");
        }

        public object GetFlaggedProperty(Flag flag)
        {
            foreach (Property p in _values.Keys)
                if (p.Flags.Contains(flag))
                    return _values[p];

            throw new ArgumentException("For this object no property that implements the given flag is defined.");
        }
    }
}
