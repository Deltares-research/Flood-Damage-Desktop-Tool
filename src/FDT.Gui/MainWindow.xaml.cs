using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.IO;


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
            ViewModel.GetBasinsDirectories = GetBasinsDirectories;
        }

        private void ParentControl_Loaded(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> availableBasins = ViewModel?.GetBasinsDirectories?.Invoke();
            ViewModel?.LoadBasins?.Execute(availableBasins);
        }

        private IEnumerable<string> GetBasinsDirectories()
        {
            if (!Directory.Exists(ViewModel.BackendPaths.ExposurePath))
            {
                ViewModel.BackendPaths.UpdateExposurePath(BrowseExposureDirectory());
            }
            if (string.IsNullOrEmpty(ViewModel.BackendPaths.ExposurePath) || !Directory.Exists(ViewModel.BackendPaths.ExposurePath))
            {
                Close();
            }
            return GuiUtils.GetSubDirectoryNames(Directory.GetDirectories(ViewModel.BackendPaths.ExposurePath));
        }

        private string BrowseExposureDirectory()
        {
            var openFileDialog = new FolderBrowserDialog();
            DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.SelectedPath))
            {
                return openFileDialog.SelectedPath;
            }
            // If the found basin names were not valid or the user did not select anything, just stop.
            return null;
        }

        private void OnRunDamageAssessmentClick(object sender, RoutedEventArgs e)
        {
            ViewModel?.RunDamageAssessment?.Execute(sender);
        }
    }
}
