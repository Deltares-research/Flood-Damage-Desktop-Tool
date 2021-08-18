using FDT.Backend.OutputLayer.IFileObjectModel;

namespace FDT.Backend.IExeHandler
{
    public interface IExeWrapper
    {
        string ExeFilePath { get; }
        void Run(IOutputData outputData);
    }
}