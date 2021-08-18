using FDT.Backend.InputOutpulLayer.IFileObjectModel;

namespace FDT.Backend.IExeHandler
{
    public interface IExeWrapper
    {
        string ExeFilePath { get; }
        void Run(IOutputData outputData);
        bool ValidateRun(IOutputData outputData);
    }
}