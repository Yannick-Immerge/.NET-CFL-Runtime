using Compiler.Foundation.Parsing;
using Runtime.Foundation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Runtime.Abstraction
{
    public class ProgramBuilder
    {
        public CFLRuntime Runtime { get; }

        private Dictionary<string, (TypeInfo, ParsedComponentAttribute)> _cache;

        public ProgramBuilder(CFLRuntime rt)
        { 
            Runtime = rt;
            _cache = new Dictionary<string, (TypeInfo, ParsedComponentAttribute)>(); 
        }

        public Container BuildFromTree(AbstractSyntaxTree tree)
            => BuildNode(tree) as Container;
        
        public object BuildNode(AbstractSyntaxNode node, object parent = null)
        {
            (TypeInfo, ParsedComponentAttribute) t = FindFitting(node.Scheme.Name);
            object o;

            //Check if component only groups specific children
            if (t.Item2.IsOwnChild)
            {
                o = BuildNode(node.Children[0], parent);
                if(t.Item1.IsInterface && t.Item1.IsAssignableFrom(o.GetType()))
                    return o;
                return null;
            }

            //Create component
            if (t.Item2.ConstructorParsing)
                o = t.Item1.GetConstructor(new Type[] { typeof(AbstractSyntaxNode), typeof(ProgramBuilder) }).Invoke(new object[] { node, this });
            else
                o = Activator.CreateInstance(t.Item1);

            //Create properties
            foreach (PropertyInfo info in t.Item1.GetProperties())
            {
                //Check for parent property
                if(info.Name == "Parent")
                {
                    if(info.CanWrite)
                        info.SetValue(o, parent);
                    continue;
                }

                //Skip unmarked properties
                if (!(info.GetCustomAttributes(typeof(ParsedPropertyAttribute)) is IEnumerable<ParsedPropertyAttribute> aCol))
                    continue;

                //Consider multiple attributes
                foreach (ParsedPropertyAttribute att in aCol)
                {
                    //Skip certain overloads
                    if (att.Options == BlockOptions.SpecificOverload && node.Overload != att.Overload)
                        continue;

                    //Parse correspondingly
                    switch (att.Type)
                    {
                        case BlockType.Node:
                            if (att.Options == BlockOptions.DirectUnfold && node.Children[att.Index].IsBrokenDown)
                            {
                                Array a = Array.CreateInstance(info.PropertyType.GenericTypeArguments[0], node.Children[att.Index].Children.Length);
                                for (int i = 0; i < a.Length; i++)
                                {
                                    AbstractSyntaxNode n = node.Children[att.Index].Children[i];
                                    a.SetValue(BuildNode(n, o), i);
                                }
                                info.SetValue(o, a);
                            }
                            else
                                info.SetValue(o, BuildNode(node.Children[att.Index], o));
                            break;
                        case BlockType.Token:
                            info.SetValue(o, node.Tokens[att.Index].Value);
                            break;
                    }

                    //Assure each property is parsed only once
                    break;
                }
            }

            //Check registration
            if (t.Item1.GetCustomAttribute(typeof(RegisteredComponentAttribute)) is RegisteredComponentAttribute rAtt)
                if(rAtt.Type == ComponentType.Structure)
                    Runtime.RegisterType(t.Item1.GetProperty(rAtt.NameProperty).GetValue(o) as string, o);
                else if (rAtt.Type == ComponentType.Progression)
                    Runtime.RegisterProgression(t.Item1.GetProperty(rAtt.NameProperty).GetValue(o) as string, o);
            return o;
        }

        public (TypeInfo, ParsedComponentAttribute) FindFitting(string nodeName)
        {
            if(_cache.Count == 0)
                foreach (TypeInfo i in Assembly.GetExecutingAssembly().DefinedTypes)
                {
                    if (i.GetCustomAttribute(typeof(ParsedComponentAttribute)) is ParsedComponentAttribute att)
                        _cache.Add(att.NodeName, (i, att));
                }

            return _cache[nodeName];
        }
    }
}
