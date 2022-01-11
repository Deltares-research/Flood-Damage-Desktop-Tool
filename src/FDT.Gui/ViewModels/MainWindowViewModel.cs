using System;
using System.Collections.Generic;
using FDT.Gui.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FDT.Backend.DomainLayer.DataModel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.ServiceLayer.ExeHandler;

namespace FDT.Gui.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<string> _availableBasins;
        private Dictionary<string, IBasin> _basinDictionary;
        private AssessmentStatus _runStatus;

        public MainWindowViewModel()
        {
            BasinScenarios = new ObservableCollection<IBasinScenario>();
            _basinDictionary = new Dictionary<string, IBasin>();
            SelectRootDirectory = new RelayCommand(OnSelectRootDirectory);
            RunDamageAssessment = new RelayCommand(OnRunDamageAssessment);
            BackendPaths = new ApplicationPaths();
            RunStatus = AssessmentStatus.LoadingBasins;
            AvailableBasins = new ObservableCollection<string>();
        }

        private void InitializeDefaultBasinScenarios()
        {
            BasinScenarios.Add(new EventBasedScenario() { GetDefaultHazardDirectory = GetHazardPath });
            BasinScenarios.Add(new RiskBasedScenario() { GetDefaultHazardDirectory = GetHazardPath });
            foreach (var basinScenario in BasinScenarios)
            {
                basinScenario.AddExtraScenario();
            }
        }

        public AssessmentStatus RunStatus
        {
            get => _runStatus;
            set
            {
                _runStatus = value;
                OnPropertyChanged();
            }
        }

        private Func<string> GetHazardPath
        {
            get
            {
                return () => BackendPaths.HazardPath;
            }
        }

        public IApplicationPaths BackendPaths { get; set; }

        public ObservableCollection<string> AvailableBasins
        {
            get => _availableBasins;
            private set
            {
                _availableBasins = value;
                OnPropertyChanged();
                RunStatus = _availableBasins != null && _availableBasins.Any() ? AssessmentStatus.Ready : AssessmentStatus.LoadingBasins;
                if (_availableBasins != null && _availableBasins.Any())
                    SelectedBasin = AvailableBasins.FirstOrDefault();
            } 
        }

        public string SelectedBasin
        {
            get => BackendPaths?.SelectedBasin?.BasinName ?? string.Empty;
            set
            {
                BackendPaths.SelectedBasin = _basinDictionary[value];
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IBasinScenario> BasinScenarios { get; }

        public ICommand SelectRootDirectory { get; }

        private void OnSelectRootDirectory(object objectCmd)
        {
            if (objectCmd is not string rootDirectory)
                throw new ArgumentNullException(nameof(rootDirectory));

            if (!Directory.Exists(rootDirectory))
                throw new DirectoryNotFoundException(rootDirectory);

            BackendPaths.ChangeRootDirectory(rootDirectory);
            _basinDictionary = BackendPaths.AvailableBasins.ToDictionary(ab => ab.BasinName, ab => ab);
            AvailableBasins = new ObservableCollection<string>(_basinDictionary.Keys);
            InitializeDefaultBasinScenarios();
        }

        public ICommand RunDamageAssessment { get; }
        
        /// <summary>
        /// CS2021: To avoid refreshing issues, the change of state (<see cref="RunStatus"/>) should be done by the
        /// command caller, instead of here.
        /// </summary>
        /// <param name="objectCmd"></param>
        private void OnRunDamageAssessment(object objectCmd)
        {
            // This method should throw any generated exception so that it's caught and handled by the caller command.
            var floodDamageDomain = new FloodDamageDomain()
            {
                FloodDamageBasinData = BackendPaths.SelectedBasin.ConvertBasin(BasinScenarios),
                Paths = BackendPaths
            };
            DamageAssessmentHandler runHandler = new DamageAssessmentHandler
            {
                DataDomain = floodDamageDomain
            };
            // The write stream seems to be causing problems when running
            // tests (check TestGivenValidRunPropertiesWhenRunDamageAssessmentThenRunStatusIsUpdated)
            // IN TEAMCITY.
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
