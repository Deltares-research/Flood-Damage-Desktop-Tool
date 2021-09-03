using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer;

namespace FDT.Backend.ServiceLayer.IExeHandler
{
    public interface IRunnerHandler
    {
        IFloodDamageDomain DataDomain { get; }
        IWriter DataWriter { get; }
        IExeWrapper ExeWrapper { get; }
        void Run();
    }
}