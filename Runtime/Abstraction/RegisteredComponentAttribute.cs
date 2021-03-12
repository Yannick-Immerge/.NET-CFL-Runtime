using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Abstraction
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisteredComponentAttribute : Attribute
    {
        public string NameProperty { get; set; }

        public RegisteredComponentAttribute()
            => NameProperty = "Name";
    }
}
