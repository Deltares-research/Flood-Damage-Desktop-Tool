namespace FDT.Backend.InputOutpulLayer
{
    public interface IReader
    {
        string FilePath { get; }
        void ReadInputData();
    }
}