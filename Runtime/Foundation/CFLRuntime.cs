using Runtime.Debug;
using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime.Foundation
{
    /// <summary>
    /// The CFLRuntime simulates a machine running CFL machine code. This machine provides the heap memory and specific memory for progression and structure definitions.
    /// </summary>
    public class CFLRuntime
    {
        public static CFLRuntime CurrentRuntime { get; private set; }

        private Dictionary<string, CFLType> _typeRegister;
        private Dictionary<string, Progression> _progRegister;
        public Scope HeapScope { get; }

        public CFLRuntime()
        {
            CurrentRuntime = this;

            _typeRegister = new Dictionary<string, CFLType>();
            _progRegister = new Dictionary<string, Progression>();
        }

        public object Execute(string progName, params (string, object)[] args)
        {
            Progression p = _progRegister[progName];
            Scope s = new DebugScope(this);

            //Create parameters in scope
            bool assigned;
            foreach (Parameter a in p.Parameters)
            {
                assigned = false;
                foreach ((string, object) arg in args)
                    if (a.Name == arg.Item1)
                    {
                        s.SetValue(a.Name, arg.Item2);
                        assigned = true;
                        break;
                    }
                if (assigned)
                    continue;
                throw new ArgumentException($"The parameter {a.Name} was not assigned a value.");
            }

            return p.GetValue(s);
        }

        public Progression GetRegisteredProgression(string name)
        {
            if (_progRegister.ContainsKey(name)) 
                return _progRegister[name];
            throw new ArgumentException();
        }

        public void RegisterType(string name, object definition)
        {
            if (!_typeRegister.ContainsKey(name))
                _typeRegister.Add(name, new CFLType(name, definition));

            else
                throw new InvalidOperationException("Cannot register two types with the same name.");
        }

        public CFLType GetRegisteredType(string name)
        {
            if (_typeRegister.ContainsKey(name))
                return _typeRegister[name];

            else
                return null;
        }

        public bool ContainsType(string name)
            => _typeRegister.ContainsKey(name);

        public void RegisterProgression(string name, object definition)
        {
            if (!_progRegister.ContainsKey(name) && definition is Progression p)
                _progRegister.Add(name, p);
            else
                throw new InvalidOperationException("Cannot register two progressions with the same name.");
        }
    }
}
