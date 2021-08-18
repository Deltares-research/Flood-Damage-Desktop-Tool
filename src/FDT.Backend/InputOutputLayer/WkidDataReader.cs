﻿using System;
using System.IO;

namespace FDT.Backend.InputOutputLayer
{
    public class WkidDataReader: IReader
    {
        public static readonly string WkidFileName = "WKID.txt";
        public string BasinDir { get; set; }
        public string FilePath => Path.Combine(BasinDir, WkidFileName);
        private string _wkidCode;
        public string ReadInputData()
        {
            if (string.IsNullOrEmpty(BasinDir))
                throw new ArgumentNullException(nameof(BasinDir));

            using (var readStream = new StreamReader(FilePath))
            {
                _wkidCode = readStream.ReadToEnd().Trim();
            }

            return _wkidCode;
        }
    }
}