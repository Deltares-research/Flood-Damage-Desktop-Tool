using System.ComponentModel;

namespace FDT.Gui
{
    public interface IFloodMap : INotifyPropertyChanged
    {
        string MapPath { get; }
        bool HasReturnPeriod { get; }
        int ReturnPeriod { get; }
    }
}