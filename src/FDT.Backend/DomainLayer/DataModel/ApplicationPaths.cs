using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIAT.Backend.DomainLayer.IDataModel;
using Path = System.IO.Path;

namespace FIAT.Backend.DomainLayer.DataModel
{
    public class ApplicationPaths: IApplicationPaths
    {
        private string _rootPath;

        public ApplicationPaths()
        {
            // This software will be placed under.
            // Root >> System >> GUI
            // We define here the 'default' paths.
            string guiDirectory = Environment.CurrentDirectory;
            DirectoryInfo rootDirectory = Directory.GetParent(guiDirectory)?.Parent;
            _rootPath = rootDirectory?.FullName ?? string.Empty;
            AvailableBasins = new List<IBasin>();
        }
        public IEnumerable<IBasin> AvailableBasins { get; private set; }
        public IBasin SelectedBasin { get; set; }
        public string RootPath => _rootPath;
        public string DatabasePath => Path.Combine(RootPath, "database");
        public string ExposurePath => Path.Combine(DatabasePath, "Exposure");
        public string HazardPath => Path.Combine(DatabasePath, "Hazard");
        public string SystemPath => Path.Combine(RootPath, "System");
        public string ResultsPath => Path.Combine(RootPath, "Results");

        public void ChangeRootDirectory(string rootDirectory)
        {
            _rootPath = rootDirectory;
            IEnumerable<string> exposureSubDirs = GetExposureSubDirectories();
            AvailableBasins = exposureSubDirs.Select(BasinUtils.GetBasin).ToArray();
        }

        private IEnumerable<string> GetExposureSubDirectories()
        {
            if (!Directory.Exists(ExposurePath))
                throw new DirectoryNotFoundException($"Exposure directory does not exist at {ExposurePath}.");
            var directories = Directory.GetDirectories(ExposurePath);
            if (!directories.Any())
                throw new Exception($"No basin subdirectories found at Exposure directory {ExposurePath}");
            return directories;
        }
    }
}