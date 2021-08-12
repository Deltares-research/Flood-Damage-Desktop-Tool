using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FDT.Gui.ViewModels
{
    public interface IScenario: INotifyPropertyChanged
    {
        string ScenarioName { get; set; }
        ObservableCollection<IFloodMap> FloodMaps { get; set; }
        void AddExtraFloodMap();
    }
}