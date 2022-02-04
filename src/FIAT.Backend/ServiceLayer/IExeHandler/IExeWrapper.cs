using FIAT.Backend.PersistenceLayer.FileObjectModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;

namespace FIAT.Backend.ServiceLayer.IExeHandler
{
    public interface IExeWrapper
    {
        void Run(IOutputData outputData);
        ValidationReport GetValidationReport();
    }
}