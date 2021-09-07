using System.Windows;
using System.Windows.Controls;
using FDT.Gui.ViewModels;
using Microsoft.Win32;

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

        private void OnOpenFileDialog(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            const string fileExtension = ".tif";
            // extension = .tif
            openFileDialog.InitialDirectory = FloodMap?.GetDefaultHazardDirectory?.Invoke();
            openFileDialog.Filter = $"Flood Map File (*{fileExtension}) | *{fileExtension}";
            openFileDialog.DefaultExt = fileExtension;
            bool? showDialog = openFileDialog.ShowDialog();
            if (showDialog != null && (bool) showDialog)
            {
                FloodMap.MapPath = openFileDialog.FileName;
            }
        }
    }
}
