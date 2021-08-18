using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer;

namespace FDT.Backend.IExeHandler
{
    public interface IRunnerHandler
    {
        IFloodDamageDomain DataDomain { get; }
        IWriter DataWriter { get; }
        IExeWrapper ExeWrapper { get; }
        void Run();
    }
}