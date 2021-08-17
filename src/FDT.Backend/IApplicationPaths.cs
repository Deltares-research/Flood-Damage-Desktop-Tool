namespace FDT.Backend
{
    public interface IApplicationPaths
    {
        string RootPath { get; set; }
        string DatabasePath { get; }
        string ExposurePath { get; }
        string SystemPath { get; }
        string ResultsPath { get; }
        void UpdateExposurePath(string exposurePath);
    }
}