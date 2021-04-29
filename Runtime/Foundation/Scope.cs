using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    /// <summary>
    /// Specifies a stack and heap (defined in the <see cref="CFLRuntime"/>) for usage in a specific area of code execution.
    /// </summary>
    public class Scope
    {
        public CFLRuntime Runtime { get; }

        public Scope ParentScope { get; }

        private Dictionary<VariableInfo, object> _stack;
        private Dictionary<string, VariableInfo> _vStack;

        public Scope(CFLRuntime runtime, Scope parent)
        {
            Runtime = runtime;
            _stack = new Dictionary<VariableInfo, object>();
            _vStack = new Dictionary<string, VariableInfo>();
        }

        public bool ContainsVariable(string name)
            => _vStack.ContainsKey(name);

        public void SetMember(string member, object value)
        {
            string[] parts = member.Split('.');
            if (parts.Length == 1)
            {
                //Try get local variable
                SetValue(parts[0], value);
            }
        }

        public object RetrieveMember(string member)
        {
            string[] parts = member.Split('.');
            if(parts.Length == 1)
            {
                //Try get local variable
                if (_vStack.ContainsKey(parts[0]))
                    return GetValue(parts[0], out _);
            }

            return null;
        }

        public virtual VariableInfo GenerateVariable(string name, PrimitiveType type)
            => SetVariableAndType(name, type, false, false);

        public virtual VariableInfo GenerateVariable(string name, Structure type)
            => SetVariableAndType(name, type, false, false);

        public virtual void SetValue(string name, object value)
        { 
            VariableInfo info = SetVariableAndType(name, value.GetCFLType());
            _stack[info] = value;
        }
        
        public virtual object GetValue(string name, out CFLType type)
        {
            if (!_vStack.ContainsKey(name))
                throw new InvalidOperationException("Cannot retrieve value of undefined variable.");

            VariableInfo i = _vStack[name];
            type = i.Type;
            return _stack[i];
        }

        private VariableInfo SetVariableAndType(string name, CFLType type, bool fix = false, bool over = true)
        {
            if(_vStack.ContainsKey(name))
                if (over)
                {
                    VariableInfo i = _vStack[name];
                    i.Type = type;
                    _stack[i] = null;
                    return i;
                }
                else
                    throw new InvalidOperationException("One scope may not contain two variables with same name.");

            VariableInfo info = new VariableInfo(name, this, fix);
            info.Type = type;
            _stack.Add(info, null);
            _vStack.Add(name, info);
            return info;
        }
    }
}
