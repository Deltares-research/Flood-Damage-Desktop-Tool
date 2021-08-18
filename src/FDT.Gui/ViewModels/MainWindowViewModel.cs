using System;
using System.Collections.Generic;
using FDT.Gui.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FDT.Backend;
using FDT.Backend.DataModel;
using FDT.Backend.ExeHandler;
using FDT.Backend.OutputLayer;
using FDT.Gui.Annotations;

namespace FDT.Gui.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<string> _availableBasins;
        private string _selectedBasin;

        public MainWindowViewModel()
        {
            BasinScenarios = new ObservableCollection<IBasinScenario>();
            BasinScenarios.Add(new EventBasedScenario());
            BasinScenarios.Add(new RiskBasedScenario());

            LoadBasins = new RelayCommand(OnLoadBasins);
            RunDamageAssessment = new RelayCommand(OnRunDamageAssessment);
            BackendPaths = new ApplicationPaths();
        }

        public ApplicationPaths BackendPaths { get; }

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

        public Func<IEnumerable<string>> GetBasinsDirectories { get; set; }
        public ICommand LoadBasins { get; }

        private void OnLoadBasins(object objectCmd)
        {
            if (objectCmd is not IEnumerable<string> loadedBasins) return;

            AvailableBasins = new ObservableCollection<string>(loadedBasins);
            SelectedBasin = AvailableBasins.FirstOrDefault();
        }

        public ICommand RunDamageAssessment { get; }
        private void OnRunDamageAssessment(object objectCmd)
        {
            // This method should throw any generated exception so that it's caught and handled by the caller command.
            var floodDamageDomain = new FloodDamageDomain()
            {
                BasinData = BasinScenarios.ConvertBasin(SelectedBasin),
                Paths = BackendPaths
            };
            DamageAssessmentHandler runHandler = new DamageAssessmentHandler
            {
                DataDomain = floodDamageDomain
            };
            runHandler.Run();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
