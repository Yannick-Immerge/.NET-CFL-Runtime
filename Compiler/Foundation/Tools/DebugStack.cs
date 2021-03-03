using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Foundation.Tools
{
    public class DebugStack
    {
        public int Count { get => _stack.Count; }

        private List<int> _stack;
        
        public DebugStack()
        {
            _stack = new List<int>();
        }

        public void Push(int i)
        {
            _stack.Add(i);
            Console.WriteLine($"d: {ToString()}");
        }

        public void Pop()
        {
            _stack.RemoveAt(_stack.Count - 1);
            Console.WriteLine($"d: {ToString()}");
        }

        public override string ToString()
        {
            if (_stack.Count == 0)
                return "";

            string msg = _stack[0].ToString();
            for (int i = 1; i < _stack.Count; i++)
                msg += $", {_stack[i]}";

            return msg;
        }

        public static explicit operator Stack<int>(DebugStack d)
        {
            Stack<int> s = new Stack<int>();
            foreach (int i in d._stack)
                s.Push(i);
            return s;
        }
    }
}
