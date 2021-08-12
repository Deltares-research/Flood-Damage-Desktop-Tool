using System.Collections.ObjectModel;

namespace FDT.Gui.ViewModels
{
    class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            EventBasedScenario = new EventBasinScenario();
            RiskBasedScenario = new RiskBasinScenario();
            BasinScenarios = new ObservableCollection<IBasinScenario>();
            BasinScenarios.Add(EventBasedScenario);
            BasinScenarios.Add(RiskBasedScenario);
        }
        
        public ObservableCollection<IBasinScenario> BasinScenarios { get; }

        public IBasinScenario EventBasedScenario { get; }
        public IBasinScenario RiskBasedScenario { get; }
    }
}
