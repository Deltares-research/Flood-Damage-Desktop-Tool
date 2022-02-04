using System;
using System.Collections.ObjectModel;

namespace FIAT.Gui.ViewModels
{
    public abstract class BasinScenario<T>: IBasinScenario where T: IFloodMap, new()
    {
        public BasinScenario()
        {
            Scenarios = new ObservableCollection<IScenario>();
        }

        public virtual string ScenarioType { get; set; }

        public bool IsEnabled { get; set; }
        public Func<string> GetDefaultHazardDirectory { get; set; }
        public ObservableCollection<IScenario> Scenarios { get; }
        public void AddExtraScenario()
        {
            var sc = new Scenario<T>()
            {
                GetDefaultHazardDirectory = GetDefaultHazardDirectory
            };
            sc.AddExtraFloodMap();
            Scenarios.Add(sc);
        }
    }

    public class EventBasedScenario : BasinScenario<FloodMap>
    {
        public override string ScenarioType => "Event";
    }

    public class RiskBasedScenario : BasinScenario<FloodMapWithReturnPeriod>
    {
        public override string ScenarioType => "Risk";
    }
}