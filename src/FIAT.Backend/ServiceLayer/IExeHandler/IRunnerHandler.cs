using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer;

namespace FIAT.Backend.ServiceLayer.IExeHandler
{
    public interface IRunnerHandler
    {
        IFloodDamageDomain DataDomain { get; }
        IWriter DataWriter { get; }
        IExeWrapper ExeWrapper { get; }
        void Run();
    }
}