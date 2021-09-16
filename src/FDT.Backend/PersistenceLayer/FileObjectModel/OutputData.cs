using System;
using System.IO;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.PersistenceLayer.FileObjectModel
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
            if (string.IsNullOrEmpty(BasinName))
                throw new ArgumentNullException(nameof(IOutputData.BasinName));
            if (string.IsNullOrEmpty(ScenarioName))
                throw new ArgumentNullException(nameof(IOutputData.ScenarioName));
        }

    }
}