namespace FDT.Backend.IExeHandler
{
    public interface IExeWrapper
    {
        string ExeFilePath { get; }
        void Run(string filePath);
    }
}