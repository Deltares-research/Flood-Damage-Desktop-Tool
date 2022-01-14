using System;
using System.Collections.Generic;
using FDT.Gui.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FDT.Backend;
using FDT.Backend.DomainLayer.DataModel;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.ServiceLayer.ExeHandler;
using FDT.Gui.Properties;

namespace FDT.Gui.ViewModels
{
    public sealed class MainWindowViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<string> _availableBasins;
        private Dictionary<string, IBasin> _basinDictionary;
        private readonly SelectBasinHelper _selectBasinHelper;
        private AssessmentStatus _runStatus;
        public string SelectAreaOfInterestLabel
        {
            get => Resources.MainWindow_SelectAreaOfInterest_Label;
        }
        public string SelectRootDirectoryButtonText
        {
            get => Resources.MainWindow_SelectRootDirectory_Button;
        }
        public string SaveShapefileLabel
        {
            get => Resources.MainWindowViewModel_SaveShapefileLabel;
        }
        
        public MainWindowViewModel()
        {
            _basinDictionary = new Dictionary<string, IBasin>();
            _selectBasinHelper = new SelectBasinHelper();
            BasinScenarios = new ObservableCollection<IBasinScenario>();
            AvailableBasins = new ObservableCollection<string>();
            BackendPaths = new ApplicationPaths();

            SelectRootDirectory = new RelayCommand(OnSelectRootDirectory);
            RunDamageAssessment = new RelayCommand(OnRunDamageAssessment);
            RunStatus = AssessmentStatus.LoadingBasins;
        }

        public bool SaveShapefile { get; set; }

        public Action<string> ShowWarningMessage { private get; set; }

        public AssessmentStatus RunStatus
        {
            get => _runStatus;
            set
            {
                _runStatus = value;
                OnPropertyChanged();
            }
        }

        public ApplicationPaths BackendPaths { get; set; }

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
                if (!_basinDictionary.TryGetValue(value, out var newSelection)) return;
                BackendPaths.SelectedBasin = newSelection;
                var warningMessage = _selectBasinHelper.GetSelectedBasinWarning(newSelection);
                if(!string.IsNullOrEmpty(warningMessage))
                    ShowWarningMessage?.Invoke(warningMessage);

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

        public ObservableCollection<IBasinScenario> BasinScenarios { get; }

        public ICommand SelectRootDirectory { get; }
        public ICommand RunDamageAssessment { get; }

        private void InitializeDefaultBasinScenarios()
        {
            BasinScenarios.Add(new EventBasedScenario() { GetDefaultHazardDirectory = GetHazardPath });
            BasinScenarios.Add(new RiskBasedScenario() { GetDefaultHazardDirectory = GetHazardPath });
            foreach (var basinScenario in BasinScenarios)
            {
                basinScenario.AddExtraScenario();
            }
        }

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

        /// <summary>
        /// CS2021: To avoid refreshing issues, the change of state (<see cref="RunStatus"/>) should be done by the
        /// command caller, instead of here.
        /// </summary>
        /// <param name="objectCmd"></param>
        private void OnRunDamageAssessment(object objectCmd)
        {
            // This method should throw any generated exception so that it's caught and handled by the caller command.
            DamageAssessmentHandler runHandler = new DamageAssessmentHandler
            {
                DataDomain = new FloodDamageDomain
                {
                    FloodDamageBasinData = BackendPaths.SelectedBasin.ConvertBasin(BasinScenarios.ConvertBasinScenarios()),
                    Paths = BackendPaths
                },
                WriteShpOutput = SaveShapefile
            };
            // The write stream seems to be causing problems when running
            // tests (check TestGivenValidRunPropertiesWhenRunDamageAssessmentThenRunStatusIsUpdated)
            // IN TEAMCITY.
            runHandler.Run();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
