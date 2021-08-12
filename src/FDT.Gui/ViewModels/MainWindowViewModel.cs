using System.Collections.Generic;
using FDT.Gui.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FDT.Gui.Annotations;

namespace FDT.Gui.ViewModels
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<string> _availableBasins;
        private string _selectedBasin;

        public MainWindowViewModel()
        {
            BasinScenarios = new ObservableCollection<IBasinScenario>();

            EventBasedScenario = new BasinScenario<FloodMap>()
            {
                ScenarioType = "Event"
            };
            RiskBasedScenario = new BasinScenario<FloodMapWithReturnPeriod>()
            {
                ScenarioType = "Risk"
            };
            
            BasinScenarios.Add(EventBasedScenario);
            BasinScenarios.Add(RiskBasedScenario);

            LoadBasins = new RelayCommand(OnLoadBasins);
        }

        public ObservableCollection<string> AvailableBasins
        {
            get => _availableBasins;
            private set
            {
                _availableBasins = value;
                OnPropertyChanged();
            } 
        }

        public string SelectedBasin
        {
            get => _selectedBasin;
            set
            {
                _selectedBasin = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IBasinScenario> BasinScenarios { get; }

        private IBasinScenario EventBasedScenario { get; }
        private IBasinScenario RiskBasedScenario { get; }

        public ICommand LoadBasins { get; }

        private void OnLoadBasins(object objectCmd)
        {
            if (objectCmd is IEnumerable<string> loadedBasins)
            {
                AvailableBasins = new ObservableCollection<string>(loadedBasins);
                SelectedBasin = AvailableBasins.FirstOrDefault();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
