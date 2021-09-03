namespace FDT.Backend.PersistenceLayer
{
    public interface IReader
    {
        string FilePath { get; }
        string ReadInputData();
    }
}