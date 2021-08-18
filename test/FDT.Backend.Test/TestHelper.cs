using System;
using System.IO;
using System.Reflection;

namespace FDT.Backend.Test
{
    public static class TestHelper
    {
        /// <summary>
        /// https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in
        /// </summary>
        internal static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        internal static string TestRootDirectory
        {
            get
            {
                return Path.Combine(AssemblyDirectory, "TestData", "TestRoot");
            }
        }

        internal static string TestConfigurationFile
        {
            get
            {
                return Path.Combine(TestRootDirectory, "database", "example_filled_configuration_file.xlsx");
            }
        }
    }
}