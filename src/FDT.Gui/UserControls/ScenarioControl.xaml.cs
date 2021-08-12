using System.Windows;
using System.Windows.Controls;
using FDT.Gui.ViewModels;

namespace FDT.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for ScenarioControl.xaml
    /// </summary>
    public partial class ScenarioControl : UserControl
    {
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "Scenario",
                typeof(IScenario),
                typeof(ScenarioControl),
                new PropertyMetadata(null));

        public ScenarioControl()
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
        }

        /// <summary>
        /// This method is better to have it through the ICommand, but for now it should suffice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveFloodMapEntry(object sender, RoutedEventArgs e)
        {
            if (((Button) sender).Tag is IFloodMap floodMapEntry) Scenario.FloodMaps.Remove(floodMapEntry);
        }
    }
}
