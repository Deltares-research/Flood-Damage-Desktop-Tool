using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using MessageBox = System.Windows.MessageBox;


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
            try
            {
                IEnumerable<string> availableBasins = ViewModel?.GetBasinsDirectories?.Invoke();
                ViewModel?.LoadBasins?.Execute(availableBasins);
            }
            catch(Exception exception)
            {
                MessageBox.Show(
                    this,
                    $"It was not possible to find a valid Exposure directory, please check your folder structure.\nDetailed error {exception.Message}",
                    "Failed to detect directory structure.",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Close();
            }
        }

        private IEnumerable<string> GetBasinsDirectories()
        {
            if (!Directory.Exists(ViewModel.BackendPaths.ExposurePath))
            {
                ViewModel.BackendPaths.UpdateExposurePath(BrowseExposureDirectory());
            }
            
            return GuiUtils.GetSubDirectoryNames(Directory.GetDirectories(ViewModel.BackendPaths.ExposurePath));
        }

        private string BrowseExposureDirectory()
        {
            var openFileDialog = new FolderBrowserDialog();
            openFileDialog.Description = "Select the EXPOSURE directory";
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
            try
            {
                ViewModel.RunDamageAssessment.Execute(sender);
                MessageBox.Show(
                    this, 
                    $"Assessment run correctly, result files stored at {ViewModel.BackendPaths.ResultsPath}", 
                    "Successful run", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    this,
                    $"Assessment run failed, reason: {exception.Message}.\n Contact support for more details.",
                    "Failed run",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
