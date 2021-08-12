using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Linq;


namespace FDT.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ParentControl_Loaded(object sender, RoutedEventArgs e)
        {
            var availableBasins = BrowseExposureDirectory(sender, e).ToArray();
            ViewModel?.LoadBasins?.Execute(availableBasins);
        }

        private IEnumerable<string> BrowseExposureDirectory(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new FolderBrowserDialog();
            openFileDialog.Description = "Could not locate Exposure Directory, please select it.";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.SelectedPath))
            {
                var basinNames = GuiUtils.GetSubDirectoryNames(Directory.GetDirectories(openFileDialog.SelectedPath)).ToArray();
                if (basinNames.Length > 0)
                {
                    return basinNames;
                }
            }
            // If the found basin names were not valid or the user did not select anything, just stop.
            Close();
            return Enumerable.Empty<string>();
        }
    }
}
