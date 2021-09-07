using System;
using System.Windows;
using System.Windows.Forms;
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
        }

        private void OnLoadBasinsActionClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new FolderBrowserDialog();
            openFileDialog.Description = "Select the EXPOSURE directory";
            DialogResult result = openFileDialog.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK) return;
            
            try
            {
                ViewModel.LoadBasins.Execute(openFileDialog.SelectedPath);
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
