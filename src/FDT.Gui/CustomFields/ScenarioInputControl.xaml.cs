using System.Windows;
using System.Windows.Controls;
using FDT.Gui.ViewModels;

namespace FDT.Gui.CustomFields
{
    /// <summary>
    /// Interaction logic for ScenarioInputControl.xaml
    /// </summary>
    public partial class ScenarioInputControl : UserControl
    {
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "Scenario",
                typeof(IScenario),
                typeof(ScenarioInputControl),
                new PropertyMetadata(null));

        public ScenarioInputControl()
        {
            InitializeComponent();
        }

        public IScenario Scenario
        {
            get => (IScenario) GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }
    }
}
