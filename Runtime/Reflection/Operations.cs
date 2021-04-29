using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Reflection
{
    public static class Operations
    {
        public static object Operate(string op, params dynamic[] a)
        {
            switch (op)
            {
                case "+":
                    return a[0] + a[1];
                case "-":
                    return a[0] - a[1];
                case "*":
                    return a[0] * a[1];
                case "/":
                    return a[0] / a[1];
                case "%":
                    return a[0] % a[1];
            }

            throw new NotSupportedException($"The operator '{op}' is not supported.");
        }

        public static int Priority(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 0;
                case "*":
                case "/":
                    return 1;
                case "%":
                    return -1;
            }

            throw new NotSupportedException($"The operator '{op}' is not supported.");
        }
    }
}
