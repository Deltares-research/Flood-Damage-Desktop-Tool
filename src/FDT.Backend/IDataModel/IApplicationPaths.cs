namespace FDT.Backend.IDataModel
{
    public interface IApplicationPaths
    {
        string RootPath { get; set; }
        string DatabasePath { get; }
        string ExposurePath { get; }
        string SelectedBasinPath { get; }
        string SystemPath { get; }
        string ResultsPath { get; }
        void UpdateExposurePath(string exposurePath);
        void UpdateSelectedBasin(string selectedBasin);
    }
}