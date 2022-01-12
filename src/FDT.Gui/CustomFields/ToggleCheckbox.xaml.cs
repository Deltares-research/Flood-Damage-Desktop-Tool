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

namespace FDT.Gui.CustomFields
{
    /// <summary>
    /// Interaction logic for ToggleCheckbox.xaml
    /// Original code extracted from https://github.com/TacticDevGit/C-WPf-Toggle-Switch-UI-Control/tree/master/ToggleSwitch/ToggleSwitch
    /// </summary>
    public partial class ToggleCheckbox : UserControl
    {
        Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -39, 0);
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(165, 165, 165));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(0, 159, 221));

        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "Toggled",
                typeof(bool),
                typeof(ToggleCheckbox),
                new PropertyMetadata(null));

        public ToggleCheckbox()
        {
            InitializeComponent();
            Back.Fill = Off;
            Dot.Margin = LeftSide;
        }

        public bool Toggled
        {
            get => (bool)GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;
            }
            else
            {
                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;
            }
            else
            {
                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;

            }
        }
    }
}
