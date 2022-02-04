using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Gui.Properties;

namespace FIAT.Gui.ViewModels
{
    public class Scenario<T>: IScenario where T : IFloodMap, new()
    {
        public Scenario()
        {
            FloodMaps = new ObservableCollection<IFloodMap>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public string ScenarioName { get; set; }
        public FloodMapType ScenarioFloodMapType { get; set; }
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
                return string.IsNullOrEmpty(ScenarioName) ? Resources.Scenario_this_Scenario_name_is_required : null;
            }
        }
    }
}