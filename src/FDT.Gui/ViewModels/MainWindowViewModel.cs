using System.Collections.ObjectModel;

namespace FDT.Gui.ViewModels
{
    class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            BasinScenarios = new ObservableCollection<IBasinScenario>();

            EventBasedScenario = new BasinScenario<FloodMap>();
            RiskBasedScenario = new BasinScenario<FloodMapWithReturnPeriod>();
            
            BasinScenarios.Add(EventBasedScenario);
            BasinScenarios.Add(RiskBasedScenario);
        }
        
        public ObservableCollection<IBasinScenario> BasinScenarios { get; }

        public IBasinScenario EventBasedScenario { get; }
        public IBasinScenario RiskBasedScenario { get; }
    }
}
