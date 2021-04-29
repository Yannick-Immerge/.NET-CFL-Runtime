using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Abstraction
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisteredComponentAttribute : Attribute
    {
        public string NameProperty { get; set; }
        public ComponentType Type { get; }

        public RegisteredComponentAttribute(ComponentType type)
        { 
            NameProperty = "Name";
            Type = type;
        }
    }

    public enum ComponentType
    {
        Progression,
        Structure
    }
}
