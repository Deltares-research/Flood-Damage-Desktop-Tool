using System;
using System.Windows;
using System.Windows.Forms;
using FIAT.Gui.ViewModels;
using MessageBox = System.Windows.MessageBox;


namespace FIAT.Gui
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

        public string LabelTest => Properties.Resources.MainWindow_SelectAreaOfInterest_Label;

        private void OnBasinSelectionChanged(string warningMessage)
        {
            MessageBox.Show(
                this,
                warningMessage,
                Properties.Resources.MainWindow_OnBasinSelectionChanged_Area_of_interest_changed,
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void OnSelectRootDirectoryClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new FolderBrowserDialog();
            openFileDialog.Description = Properties.Resources.MainWindow_OnSelectRootDirectoryClick_Select_the_database_root_directory;
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
                    string.Format(Properties.Resources.MainWindow_ExposureDirectoryNotFound_MessageError, exception.Message),
                    Properties.Resources.MainWindow_ExposureDirectoryNotFound_MessageCaption,
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
                    string.Format(Properties.Resources.MainWindow_OnAssessmentActionClick_AssessmentFinished_Message, ViewModel.BackendPaths.ResultsPath),
                    Properties.Resources.MainWindow_OnAssessmentActionClick_AssessmentFinished_Caption,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    this,
                    string.Format(Properties.Resources.MainWindow_OnAssessmentActionClick_AssessmentFailed_Message, exception.Message),
                    Properties.Resources.MainWindow_OnAssessmentActionClick_AssessmentFailed_Caption,
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
