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
    /// Interaction logic for InputFieldFloodMap.xaml
    /// </summary>
    public partial class InputFieldFloodMap : UserControl
    {
        /// <summary>
        /// Identified the Symbol dependency property
        /// </summary>
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register(
                "FloodMap",
                typeof(IFloodMap),
                typeof(InputFieldFloodMap),
                new PropertyMetadata(null));

        public InputFieldFloodMap()
        {
            InitializeComponent();
        }

        public IFloodMap FloodMap
        {
            get
            {
                return (IFloodMap) GetValue(ParameterProperty);
            }
            set
            {
                SetValue(ParameterProperty, value);
            }
        }
    }
}
