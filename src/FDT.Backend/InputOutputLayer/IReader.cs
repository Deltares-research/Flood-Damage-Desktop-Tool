namespace FDT.Backend.InputOutputLayer
{
    public interface IReader
    {
        string FilePath { get; }
        string ReadInputData();
    }
}