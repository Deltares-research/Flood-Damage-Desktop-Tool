using System;
using System.IO;
using System.Reflection;

namespace FIAT.Backend.Test
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
                string codeBase = Assembly.GetExecutingAssembly().Location;
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

        public static string TestDatabaseDirectory
        {
            get
            {
                return Path.Combine(TestRootDirectory, "database");
            }
        }

        internal static string TestConfigurationFile
        {
            get
            {
                return Path.Combine(TestDatabaseDirectory, "example_filled_configuration_file.xlsx");
            }
        }
    }
}