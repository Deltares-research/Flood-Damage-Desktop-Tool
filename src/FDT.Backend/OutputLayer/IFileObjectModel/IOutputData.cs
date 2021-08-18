namespace FDT.Backend.OutputLayer.IFileObjectModel
{
    public interface IOutputData
    {
        string FilePath { get; }
        string BasinName { get; }
        string ScenarioName { get; }
    }
}