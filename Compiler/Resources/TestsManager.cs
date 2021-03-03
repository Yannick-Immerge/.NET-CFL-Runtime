using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compiler.Resources
{
    public static class TestsManager
    {
        private static Dictionary<string, FileInfo> _tests;
        private static DirectoryInfo _testsDir;

        public static void LoadTests()
        {
            //Load specs directory
            if (_testsDir == null)
                _testsDir = new DirectoryInfo(@"Tests");
            if (!_testsDir.Exists)
                throw new IOException($"The default tests directory at { _testsDir.FullName } does not exist.");

            //Iterate for test cf-Files
            _tests = new Dictionary<string, FileInfo>();
            foreach (FileInfo i in _testsDir.EnumerateFiles())
                if (i.Extension == ".cf")
                    _tests.Add(i.Name.Substring(0, i.Name.LastIndexOf('.')), i);
        }

        public static FileInfo GetTest(string name)
        {
            if (_tests == null)
                LoadTests();
            if (!_tests.ContainsKey(name)) 
                return null;
            return _tests[name];
        }
    }
}
