using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.OutputLayer.FileObjectModel
{
    public class OutputData : IOutputData
    {
        public string FilePath { get; set; }
        public string BasinName { get; set; }
        public string ScenarioName { get; set; }
    }
}