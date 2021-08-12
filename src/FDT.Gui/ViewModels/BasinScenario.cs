using System.Collections.ObjectModel;

namespace FDT.Gui.ViewModels
{
    public class BasinScenario<T>: IBasinScenario where T: IFloodMap, new()
    {
        public BasinScenario()
        {
            Scenarios = new ObservableCollection<IScenario>();
            AddExtraScenario();
        }
        public bool IsEnabled { get; set; }

        public ObservableCollection<IScenario> Scenarios { get; }
        public void AddExtraScenario()
        {
            Scenarios.Add(new Scenario<T>());
        }
    }
}