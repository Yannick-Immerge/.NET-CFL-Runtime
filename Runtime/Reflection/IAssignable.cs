using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    public interface IAssignable : IValuable
    {
        public void AssignValue(object value);
    }
}
