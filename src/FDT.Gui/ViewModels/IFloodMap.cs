using System;
using System.ComponentModel;

namespace FIAT.Gui.ViewModels
{
    public interface IFloodMap : INotifyPropertyChanged, IDataErrorInfo
    {
        string MapPath { get; set; }
        bool HasReturnPeriod { get; }
        int ReturnPeriod { get; set; }
        Func<string> GetDefaultHazardDirectory { get; set; }
    }
}