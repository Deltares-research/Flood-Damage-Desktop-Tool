using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FDT.Gui.ViewModels
{
    public class Scenario<T>: IScenario where T : IFloodMap, new()
    {
        public Scenario()
        {
            FloodMaps = new ObservableCollection<IFloodMap>();
            AddExtraFloodMap();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public string ScenarioName { get; set; }
        public ObservableCollection<IFloodMap> FloodMaps { get; set; }
        public void AddExtraFloodMap()
        {
            FloodMaps.Add(new T());
        }

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                if (columnName != nameof(ScenarioName)) return null;
                return string.IsNullOrEmpty(ScenarioName) ? "Scenario Name is required" : null;
            }
        }
    }
}