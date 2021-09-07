namespace FDT.Backend.DomainLayer.IDataModel
{
    public interface IApplicationPaths
    {
        string RootPath { get; set; }
        string DatabasePath { get; }
        string ExposurePath { get; }
        string HazardPath { get; }
        string SelectedBasinPath { get; }
        string SystemPath { get; }
        string ResultsPath { get; }
        void UpdateSelectedBasin(string selectedBasin);
    }
}