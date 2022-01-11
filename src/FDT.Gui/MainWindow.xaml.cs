using System;
using System.Windows;
using System.Windows.Forms;
using FDT.Gui.ViewModels;
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
            ViewModel.ShowWarningMessage = OnBasinSelectionChanged;
        }

        private void OnBasinSelectionChanged(string warningMessage)
        {
            MessageBox.Show(
                this,
                warningMessage,
                "Area of interest changed",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void OnSelectRootDirectoryClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new FolderBrowserDialog();
            openFileDialog.Description = "Select the database root directory";
            DialogResult result = openFileDialog.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK) return;
            
            try
            {
                ViewModel.SelectRootDirectory.Execute(openFileDialog.SelectedPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    this,
                    $"It was not possible to find a valid Exposure directory, please check your folder structure.\nDetailed error {exception.Message}",
                    "Failed to detect directory structure.",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }

        private void OnAssessmentActionClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.RunStatus = AssessmentStatus.Running;
                InvalidateVisual();
                ViewModel.RunDamageAssessment.Execute(sender);
                MessageBox.Show(
                    this,
                    $"Assessment run finished.\nFor more details check the results directory at {ViewModel.BackendPaths.ResultsPath}",
                    "Finished run",
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
            finally
            {
                ViewModel.RunStatus = AssessmentStatus.Ready;
            }
        }
    }
}
