using System;
using System.IO;
using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.OutputLayer.FileObjectModel
{
    public class OutputData : IOutputData
    {
        public string ConfigurationFilePath { get; set; }
        public string BasinName { get; set; }
        public string ScenarioName { get; set; }
        public void ValidateParameters()
        {
            if (string.IsNullOrEmpty(ConfigurationFilePath))
                throw new ArgumentNullException(nameof(IOutputData.ConfigurationFilePath));
            if (!File.Exists(ConfigurationFilePath))
                throw new FileNotFoundException(ConfigurationFilePath);
            if (string.IsNullOrEmpty(BasinName))
                throw new ArgumentNullException(nameof(IOutputData.BasinName));
            if (string.IsNullOrEmpty(ScenarioName))
                throw new ArgumentNullException(nameof(IOutputData.ScenarioName));
        }
    }
}