using System;
using System.IO;
using FDT.Backend.DomainLayer.IDataModel;
using Path = System.IO.Path;

namespace FDT.Backend.DomainLayer.DataModel
{
    public class ApplicationPaths: IApplicationPaths
    {
        private string _selectedBasin;

        public ApplicationPaths()
        {
            // This software will be placed under.
            // Root >> System >> GUI
            // We define here the 'default' paths.
            string guiDirectory = Environment.CurrentDirectory;
            DirectoryInfo rootDirectory = Directory.GetParent(guiDirectory)?.Parent;
            RootPath = rootDirectory?.FullName ?? string.Empty;
        }
        public string RootPath { get; set; }
        public string DatabasePath => Path.Combine(RootPath, "database");
        public string ExposurePath => Path.Combine(DatabasePath, "Exposure");
        public string HazardPath => Path.Combine(DatabasePath, "Hazard");

        public string SelectedBasinPath => Path.Combine(ExposurePath, _selectedBasin);

        public string SystemPath => Path.Combine(RootPath, "System");
        public string ResultsPath => Path.Combine(RootPath, "Results");
        public void UpdateExposurePath(string exposurePath)
        {
            if (string.IsNullOrEmpty(exposurePath))
                throw new ArgumentNullException(nameof(exposurePath));
            if (!Directory.Exists(exposurePath))
                throw new DirectoryNotFoundException(exposurePath);
            RootPath = Directory.GetParent(exposurePath)?.Parent?.FullName;
        }

        public void UpdateSelectedBasin(string selectedBasin)
        {
            _selectedBasin = selectedBasin;
        }
    }
}