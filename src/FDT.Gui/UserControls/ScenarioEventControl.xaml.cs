using System.Windows;
using System.Windows.Controls;
using FDT.Gui.ViewModels;

namespace FDT.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for ScenarioEventControl.xaml
    /// </summary>
    public partial class ScenarioEventControl : UserControl
    {
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "Scenario",
                typeof(IScenario),
                typeof(ScenarioEventControl),
                new PropertyMetadata(null));

        public ScenarioEventControl()
        {
            InitializeComponent();
        }

        public IScenario Scenario
        {
            get => (IScenario)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }
    }
}
