using System;
using System.IO;

namespace FDT.Backend.InputOutputLayer
{
    public class WkidDataReader: IReader
    {
        public static readonly string WkidFileName = "WKID.txt";
        public string BasinDir { get; private set; }
        public string FilePath => Path.Combine(BasinDir, WkidFileName);
        private string _wkidCode;
        public void ReadInputData()
        {
            if (BasinDir == null)
                throw new ArgumentNullException(nameof(BasinDir));

            using (var readStream = new StreamReader(FilePath))
            {
                _wkidCode = readStream.ReadToEnd().Trim();
            }
        }

        public string GetWkidCode(string basinDir)
        {
            if (string.IsNullOrEmpty(basinDir))
                throw new ArgumentNullException(nameof(basinDir));
            BasinDir = basinDir;
            ReadInputData();
            return _wkidCode;
        }
    }
}