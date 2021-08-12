using System.ComponentModel;

namespace FDT.Gui.ViewModels
{
    public interface IFloodMap : INotifyPropertyChanged
    {
        string MapPath { get; set; }
        bool HasReturnPeriod { get; }
        int ReturnPeriod { get; set; }
    }
}