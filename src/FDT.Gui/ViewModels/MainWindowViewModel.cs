using FDT.Gui.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

            LoadBasins = new RelayCommand(OnLoadBasins);
    }
        
        public ObservableCollection<IBasinScenario> BasinScenarios { get; }

        public IBasinScenario EventBasedScenario { get; }
        public IBasinScenario RiskBasedScenario { get; }

        public ICommand LoadBasins { get; }

        private void OnLoadBasins(object objectCmd)
        {

        }
    }
}
