using FDT.Backend.IDataModel;

namespace FDT.Backend.IExeHandler
{
    public interface IRunnerHandler
    {
        IFloodDamageDomain DataDomain { get; }
        IExeWrapper ExeWrapper { get; }
        void Run();
    }
}