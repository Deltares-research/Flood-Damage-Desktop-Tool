using System;
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
            string exposureDirectory = GetExposureDirectory();
            return GuiUtils.GetSubDirectoryNames(Directory.GetDirectories(exposureDirectory));
        }

        private string GetExposureDirectory()
        {
            // Database >> Exposure
            // Database >> System >> FDT.exe
            string exposureDirectory;
            try
            {
                DirectoryInfo directoryInfo = Directory.GetParent(Environment.CurrentDirectory)?.Parent;
                string databaseDirectory = directoryInfo?.FullName ?? string.Empty;
                exposureDirectory = Path.Combine(databaseDirectory, "Exposure");
                if (!Directory.Exists(exposureDirectory))
                    throw new DirectoryNotFoundException("Default Exposure path not found. Manually entry required.");
            }
            catch (Exception)
            {
                exposureDirectory = BrowseExposureDirectory();
            }

            if (string.IsNullOrEmpty(exposureDirectory) || !Directory.Exists(exposureDirectory))
            {
                Close();
            }

            return exposureDirectory;
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
    }
}
