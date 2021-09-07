using System;
using FDT.Gui.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using FDT.Backend.DomainLayer.DataModel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.ServiceLayer.ExeHandler;

namespace FDT.Gui.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<string> _availableBasins;
        private string _selectedBasin;
        private AssessmentStatus _runStatus;

        public MainWindowViewModel()
        {
            BasinScenarios = new ObservableCollection<IBasinScenario>();

            LoadBasins = new RelayCommand(OnLoadBasins);
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
            get => _selectedBasin;
            set
            {
                _selectedBasin = value;
                BackendPaths.UpdateSelectedBasin(_selectedBasin);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IBasinScenario> BasinScenarios { get; }

        public ICommand LoadBasins { get; }

        private void OnLoadBasins(object objectCmd)
        {
            if (objectCmd is not string exposurePath)
                throw new ArgumentNullException(nameof(exposurePath));

            BackendPaths.UpdateExposurePath(exposurePath);
            if (!Directory.Exists(BackendPaths.ExposurePath))
                throw new DirectoryNotFoundException(exposurePath);

            string[] subDirectoryNames = GuiUtils.GetSubDirectoryNames(Directory.GetDirectories(BackendPaths.ExposurePath)).ToArray();
            if (!subDirectoryNames.Any())
                throw new Exception($"No basin subdirectories found at Exposure directory {exposurePath}");
            AvailableBasins = new ObservableCollection<string>(subDirectoryNames);
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
                BasinData = BasinScenarios.ConvertBasin(BackendPaths.SelectedBasinPath),
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
