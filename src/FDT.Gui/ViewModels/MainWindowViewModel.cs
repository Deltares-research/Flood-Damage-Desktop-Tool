using System;
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
        private string _selectedBasin;
        private AssessmentStatus _runStatus;

        public MainWindowViewModel()
        {
            BasinScenarios = new ObservableCollection<IBasinScenario>();
            BasinScenarios.Add(new EventBasedScenario());
            BasinScenarios.Add(new RiskBasedScenario());

            LoadBasins = new RelayCommand(OnLoadBasins);
            RunDamageAssessment = new RelayCommand(OnRunDamageAssessment);
            BackendPaths = new ApplicationPaths();
            RunStatus = AssessmentStatus.LoadingBasins;
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

        public IApplicationPaths BackendPaths { get; set; }

        public ObservableCollection<string> AvailableBasins
        {
            get => _availableBasins;
            private set
            {
                _availableBasins = value;
                OnPropertyChanged();
                RunStatus = _availableBasins != null && _availableBasins.Any() ? AssessmentStatus.Ready : AssessmentStatus.LoadingBasins;
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
            SelectedBasin = AvailableBasins.FirstOrDefault();
        }

        public ICommand RunDamageAssessment { get; }
        private void OnRunDamageAssessment(object objectCmd)
        {
            // This method should throw any generated exception so that it's caught and handled by the caller command.
            try
            {
                var floodDamageDomain = new FloodDamageDomain()
                {
                    BasinData = BasinScenarios.ConvertBasin(BackendPaths.SelectedBasinPath),
                    Paths = BackendPaths
                };
                DamageAssessmentHandler runHandler = new DamageAssessmentHandler
                {
                    DataDomain = floodDamageDomain
                };
                RunStatus = AssessmentStatus.Running;
                runHandler.Run();
            }
            catch
            {
                throw;
            }
            finally
            {
                RunStatus = AssessmentStatus.Ready;
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
