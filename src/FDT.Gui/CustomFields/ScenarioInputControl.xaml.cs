using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
