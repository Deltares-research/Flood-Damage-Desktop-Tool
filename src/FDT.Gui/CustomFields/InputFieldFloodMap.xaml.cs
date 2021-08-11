using System.Windows;
using System.Windows.Controls;

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
            get => (IFloodMap) GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }
    }
}
