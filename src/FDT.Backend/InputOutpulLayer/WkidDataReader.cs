using System;
using System.IO;

namespace FDT.Backend.InputOutpulLayer
{
    public class WkidDataReader: IReader
    {
        public string BasinDir { get; private set; }
        public string FilePath => Path.Combine(BasinDir, "WKID.txt");
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
                throw new ArgumentNullException(basinDir);
            BasinDir = basinDir;
            ReadInputData();
            return _wkidCode;
        }
    }
}