using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FIAT.Gui.ViewModels;

namespace FIAT.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for BasinRiskScenarioControl.xaml
    /// </summary>
    public partial class BasinRiskScenarioControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "BasinScenario",
                typeof(IBasinScenario),
                typeof(BasinRiskScenarioControl),
                new PropertyMetadata(null));

        public BasinRiskScenarioControl()
        {
            InitializeComponent();
        }

        public IBasinScenario BasinScenario
        {
            get => (IBasinScenario)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }

        public bool CanRemoveScenarioEntries
        {
            get { return BasinScenario.Scenarios.Count > 1; }
        }

        private void AddNewScenario(object sender, RoutedEventArgs e)
        {
            BasinScenario?.AddExtraScenario();
            OnPropertyChanged(nameof(CanRemoveScenarioEntries));
        }

        private void RemoveScenario(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is IScenario scenario) BasinScenario.Scenarios.Remove(scenario);
            OnPropertyChanged(nameof(CanRemoveScenarioEntries));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
