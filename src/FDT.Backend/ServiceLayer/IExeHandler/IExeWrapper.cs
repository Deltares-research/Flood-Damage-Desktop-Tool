using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.ServiceLayer.IExeHandler
{
    public interface IExeWrapper
    {
        string ExeFilePath { get; }
        void Run(IOutputData outputData);
        ValidationReport GetValidationReport();
    }
}