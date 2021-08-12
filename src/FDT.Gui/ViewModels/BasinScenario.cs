using System.Collections.ObjectModel;

namespace FDT.Gui.ViewModels
{
    public abstract class BasinScenario: IBasinScenario
    {
        protected BasinScenario()
        {
            Scenarios = new ObservableCollection<IScenario>();
            AddExtraScenario();
        }

        public bool IsEnabled { get; set; }
        public ObservableCollection<IScenario> Scenarios { get; set; }

        public abstract void AddExtraScenario();
    }

    public class RiskBasinScenario : BasinScenario
    {
        public override void AddExtraScenario()
        {
            Scenarios.Add(new RiskScenario());
        }
    }

    public class EventBasinScenario : BasinScenario
    {
        public override void AddExtraScenario()
        {
            Scenarios.Add(new EventScenario());
        }
    }
}