using System.Collections.Generic;
using System.IO;

namespace FDT.Gui
{
    public static class GuiUtils
    {
        public static IEnumerable<string> GetSubDirectoryNames(string[] foundSubDirectories)
        {
            if (foundSubDirectories == null || foundSubDirectories.Length == 0)
                yield break;

            foreach (string directory in foundSubDirectories)
            {
                yield return directory.Remove(0, directory.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            }
        }
    }
}