using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using FDT.Gui.Annotations;
using FDT.Gui.ViewModels;

namespace FDT.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for ScenarioRiskControl.xaml
    /// </summary>
    public partial class ScenarioRiskControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "Scenario",
                typeof(IScenario),
                typeof(ScenarioRiskControl),
                new PropertyMetadata(null));

        public ScenarioRiskControl()
        {
            InitializeComponent();
        }

        public IScenario Scenario
        {
            get => (IScenario)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }

        public bool CanRemoveFloodMapEntries
        {
            get { return Scenario.FloodMaps.Count > 1; }
        }

        private void AddFloodMapToScenario(object sender, RoutedEventArgs e)
        {
            Scenario?.AddExtraFloodMap();
            OnPropertyChanged(nameof(CanRemoveFloodMapEntries));
        }

        /// <summary>
        /// This method is better to have it through the ICommand, but for now it should suffice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveFloodMapEntry(object sender, RoutedEventArgs e)
        {
            if (((Button) sender).Tag is IFloodMap floodMapEntry) Scenario.FloodMaps.Remove(floodMapEntry);
            OnPropertyChanged(nameof(CanRemoveFloodMapEntries));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
