using System.Collections.ObjectModel;
using System.ComponentModel;
using FIAT.Backend.DomainLayer.IDataModel;

namespace FIAT.Gui.ViewModels
{
    public interface IScenario: INotifyPropertyChanged, IDataErrorInfo
    {
        string ScenarioName { get; set; }
        FloodMapType ScenarioFloodMapType { get; set; }
        ObservableCollection<IFloodMap> FloodMaps { get; set; }
        void AddExtraFloodMap();
    }
}