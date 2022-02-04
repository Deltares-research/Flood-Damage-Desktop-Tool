using System.Windows;
using System.Windows.Controls;
using FIAT.Gui.ViewModels;
using Microsoft.Win32;

namespace FIAT.Gui.CustomFields
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
            openFileDialog.Filter = string.Format(Properties.Resources.InputFieldFloodMap_OnOpenFileDialog_Flood_map_file____0_______1_, fileExtension, fileExtension);
            openFileDialog.DefaultExt = fileExtension;
            bool? showDialog = openFileDialog.ShowDialog();
            if (showDialog != null && (bool) showDialog)
            {
                FloodMap.MapPath = openFileDialog.FileName;
            }
        }
    }
}
