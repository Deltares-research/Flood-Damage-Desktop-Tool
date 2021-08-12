using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FDT.Gui.ViewModels
{
    public abstract class Scenario: IScenario
    {
        protected Scenario()
        {
            FloodMaps = new ObservableCollection<IFloodMap>();
            AddExtraFloodMap();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public string ScenarioName { get; set; }
        public ObservableCollection<IFloodMap> FloodMaps { get; set; }

        public abstract void AddExtraFloodMap();
    }

    public class RiskScenario : Scenario
    {
        public override void AddExtraFloodMap()
        {
            FloodMaps.Add(new FloodMapWithReturnPeriod());
        }
    }

    public class EventScenario : Scenario
    {
        public override void AddExtraFloodMap()
        {
            FloodMaps.Add(new FloodMap());
        }
    }
}