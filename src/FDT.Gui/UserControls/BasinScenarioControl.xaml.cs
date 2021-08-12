using System.Windows;
using System.Windows.Controls;
using FDT.Gui.ViewModels;

namespace FDT.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for BasinScenarioControl.xaml
    /// </summary>
    public partial class BasinScenarioControl : UserControl
    {
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "BasinScenario",
                typeof(IBasinScenario),
                typeof(BasinScenarioControl),
                new PropertyMetadata(null));

        public BasinScenarioControl()
        {
            InitializeComponent();
        }

        public IBasinScenario BasinScenario
        {
            get => (IBasinScenario)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }
    }
}
