using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;

namespace FDT.Backend.ServiceLayer.IExeHandler
{
    public interface IExeWrapper
    {
        void Run(IOutputData outputData);
        ValidationReport GetValidationReport();
    }
}