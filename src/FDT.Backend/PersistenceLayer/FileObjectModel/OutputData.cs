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
    }

    public static class IOutputDataExtension
    {
        /// <summary>
        /// Validates whether all the <see cref="IOutputData"/> parameters are valid.
        /// Note: This should be done before running, as the configuration file might be moved
        /// due to the .exe own behavior.
        /// </summary>
        public static void CheckValidParameters(this IOutputData outputData)
        {
            if (outputData == null)
                throw new ArgumentNullException(nameof(outputData));
            if (string.IsNullOrEmpty(outputData.BasinName))
                throw new ArgumentNullException(nameof(IOutputData.BasinName));
            if (string.IsNullOrEmpty(outputData.ScenarioName))
                throw new ArgumentNullException(nameof(IOutputData.ScenarioName));
            if (string.IsNullOrEmpty(outputData.ConfigurationFilePath))
                throw new ArgumentNullException(nameof(IOutputData.ConfigurationFilePath));
            if (!File.Exists(outputData.ConfigurationFilePath))
                throw new FileNotFoundException(outputData.ConfigurationFilePath);
        }
    }
}