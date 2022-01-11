namespace FDT.Backend.DomainLayer.IDataModel
{
    public interface IApplicationPaths
    {
        string RootPath { get;}
        string DatabasePath { get; }
        string ExposurePath { get; }
        string HazardPath { get; }
        string SystemPath { get; }
        string ResultsPath { get; }
    }
}