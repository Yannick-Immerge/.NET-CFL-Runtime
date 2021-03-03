using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Compiler.Resources
{
    public static class SpecificationsManager
    {
        private static Dictionary<string, (FileInfo Tokens, FileInfo Grammar)> _specs = null;
        private static Dictionary<string, bool> _valid = null;
        private static DirectoryInfo _specsDir = null;

        public static void LoadSpecifications()
        {
            //Load specs directory
            if(_specsDir == null)
                _specsDir = new DirectoryInfo(@"Specifications");
            if (!_specsDir.Exists)
                throw new IOException($"The default specifications directory at { _specsDir.FullName } does not exist.");

            //Iterate for tokens - grammar pairs
            _specs = new Dictionary<string, (FileInfo tokens, FileInfo grammar)>();
            _valid = new Dictionary<string, bool>();
            foreach(FileInfo i in _specsDir.EnumerateFiles())
            {
                //Create new entry for new name
                string fileName = i.Name.Substring(0, i.Name.LastIndexOf('.'));
                if (!_specs.ContainsKey(fileName))
                {
                    (FileInfo, FileInfo) s = (i.Extension == ".tokens" ? i : null, i.Extension == ".grammar" ? i : null);
                    _specs.Add(fileName, s);
                    _valid.Add(fileName, false);
                }
                //Add to existing entry
                else
                {
                    (FileInfo, FileInfo) s = _specs[fileName];
                    if (i.Extension == ".tokens")
                        s.Item1 = i;
                    else if (i.Extension == ".grammar")
                        s.Item2 = i;

                    _specs[fileName] = s;
                    _valid[fileName] = s.Item1 != null && s.Item2 != null;
                }

            }
        }

        public static (FileInfo, FileInfo)? GetSpecification(string name)
        {
            //Initialize if null
            if (_specs == null || _valid == null)
                LoadSpecifications();

            //Return valid result
            if (_valid.ContainsKey(name) && _valid[name])
                return _specs[name];

            //Return null otherwise
            return null;
        }

        public static IEnumerable<string> GetSpecsNames()
        {
            //Initialize if null
            if (_specs == null || _valid == null)
                LoadSpecifications();
            
            //Iterate through _specs
            foreach (string k in _specs.Keys)
                if (_valid[k])
                    yield return k;
        }
    }
}
