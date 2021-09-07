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
                string trimmedDirectory = directory.TrimEnd(Path.DirectorySeparatorChar);
                yield return trimmedDirectory.Remove(0, trimmedDirectory.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            }
        }
    }
}