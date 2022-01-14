namespace FDT.Backend.PersistenceLayer.IFileObjectModel
{
    public interface IOutputData
    {
        string ConfigurationFilePath { get; }
        string BasinName { get; }
        string ScenarioName { get; }
        bool SaveOutput { get; }
    }
}