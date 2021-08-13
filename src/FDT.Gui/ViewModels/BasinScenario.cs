using System.Collections.ObjectModel;

namespace FDT.Gui.ViewModels
{
    public abstract class BasinScenario<T>: IBasinScenario where T: IFloodMap, new()
    {
        public BasinScenario()
        {
            Scenarios = new ObservableCollection<IScenario>();
            AddExtraScenario();
        }

        public virtual string ScenarioType { get; set; }

        public bool IsEnabled { get; set; }

        public ObservableCollection<IScenario> Scenarios { get; }
        public void AddExtraScenario()
        {
            Scenarios.Add(new Scenario<T>());
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