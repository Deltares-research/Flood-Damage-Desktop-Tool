using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FDT.Gui.ViewModels
{
    public interface IScenario: INotifyPropertyChanged, IDataErrorInfo
    {
        string ScenarioName { get; set; }
        bool CanAddExtraFloodMaps { get; }
        ObservableCollection<IFloodMap> FloodMaps { get; set; }
        void AddExtraFloodMap();
    }
}