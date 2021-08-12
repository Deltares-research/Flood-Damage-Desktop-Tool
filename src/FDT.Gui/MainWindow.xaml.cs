using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Microsoft.Win32;

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

            DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.SelectedPath))
            {
                var directories = Directory.GetDirectories(openFileDialog.SelectedPath);
                foreach (string directory in directories)
                {
                    yield return directory.Remove(0, directory.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                }
            }
        }
    }
}
