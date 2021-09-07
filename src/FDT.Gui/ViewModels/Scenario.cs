using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FDT.Gui.ViewModels
{
    public class Scenario<T>: IScenario where T : IFloodMap, new()
    {
        public Scenario()
        {
            FloodMaps = new ObservableCollection<IFloodMap>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public string ScenarioName { get; set; }
        public bool CanAddExtraFloodMaps => typeof(T) == typeof(FloodMapWithReturnPeriod);
        public ObservableCollection<IFloodMap> FloodMaps { get; set; }
        public void AddExtraFloodMap()
        {
            FloodMaps.Add(new T(){ GetDefaultHazardDirectory = GetDefaultHazardDirectory});
        }

        public Func<string> GetDefaultHazardDirectory { get; set; }


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