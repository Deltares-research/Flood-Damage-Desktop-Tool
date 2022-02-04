using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Gui.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Gui.Test.ViewModels
{
    [TestFixture]
    public class MainWindowViewModelTest
    {
        [Test]
        public void Test_Constructor()
        {
            var viewModel = new MainWindowViewModel();
            Assert.That(viewModel, Is.InstanceOf<INotifyPropertyChanged>());
            Assert.That(viewModel.SelectRootDirectory, Is.Not.Null);
            Assert.That(viewModel.RunDamageAssessment, Is.Not.Null);
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.LoadingBasins));
            Assert.That(viewModel.BasinScenarios, Is.Empty);
            Assert.That(viewModel.BackendPaths, Is.InstanceOf<IApplicationPaths>());
            Assert.That(viewModel.AvailableBasins, Is.Empty);
        }

        public static IEnumerable InvalidExposurePath
        {
            get
            {
                yield return new TestCaseData(null, typeof(ArgumentNullException), "rootDirectory");
                yield return new TestCaseData(string.Empty, typeof(DirectoryNotFoundException), string.Empty);
                yield return new TestCaseData("InvalidPath", typeof(DirectoryNotFoundException), "InvalidPath");
                yield return new TestCaseData("In\\valid\\Path", typeof(DirectoryNotFoundException), "In\\valid\\Path");
            }
        }

        [Test]
        [TestCaseSource(nameof(InvalidExposurePath))]
        public void TestOnLoadBasinsThrowsExceptionWhenInvalidArgument(string rootDirectoryPath, Type exceptionType, string exceptionMessage)
        {
            var viewModel = new MainWindowViewModel();
            TestDelegate testAction = () => viewModel.SelectRootDirectory.Execute(rootDirectoryPath);
            Assert.That(testAction, Throws.TypeOf(exceptionType).With.Message.Contains(exceptionMessage));
        }

        [Test]
        public void TestOnLoadBasinsThrowsExceptionWhenNoSubDirectoriesFound()
        {
            // 1. Define test data.
            var viewModel = new MainWindowViewModel();
            string currentDir = Directory.GetCurrentDirectory();
            string rootDir = Path.Combine(currentDir, "testRootDir");
            string databasePath = Path.Combine(rootDir, "database");
            string exposurePath = Path.Combine(databasePath, "Exposure");
            string exceptionMessage = $"No basin subdirectories found at Exposure directory {rootDir}";

            if (Directory.Exists(rootDir))
                Directory.Delete(rootDir, true);
            Directory.CreateDirectory(exposurePath);

            var backendPaths = Substitute.For<IApplicationPaths>();
            // viewModel.BackendPaths = backendPaths;
            backendPaths.ExposurePath.Returns(rootDir);

            // 2. Define test action.
            TestDelegate testAction = () => viewModel.SelectRootDirectory.Execute(rootDir);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.TypeOf<Exception>().With.Message.Contains(exceptionMessage));
        }

        [Test]
        public void TestGivenValidRootPathWhenSelectRootDirectoryThenPathsAndBasinsAreUpdated()
        {
            // 1. Define test data.
            var viewModel = new MainWindowViewModel();
            string rootDir = Path.Combine(Directory.GetCurrentDirectory(), "testRootDir");
            string databasePath = Path.Combine(rootDir, "database");
            string exposurePath = Path.Combine(databasePath, "Exposure");
            const string availableBasin = "c-9";
            string basinPath = Path.Combine(exposurePath, availableBasin);
            if(Directory.Exists(rootDir))
                Directory.Delete(rootDir, true);
            Directory.CreateDirectory(basinPath);

            // 2. Verify initial expectations.
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.LoadingBasins));
            Assert.That(viewModel.AvailableBasins, Is.Empty);
            Assert.That(viewModel.SelectedBasin, Is.Empty);

            // 3. Define test action.
            TestDelegate testAction = () => viewModel.SelectRootDirectory.Execute(rootDir);

            // 4. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.Ready));
            Assert.That(viewModel.SelectedBasin, Is.EqualTo(availableBasin));
        }

        [Test]
        public void TestGivenValidAvailableBasinsWhenChangeSelectedBasinShowsWarningMessage()
        {
            // 1. Define test data.
            var viewModel = new MainWindowViewModel();
            string warningMssg = string.Empty;
            viewModel.ShowWarningMessage = s => warningMssg = s;
            string rootDir = Path.Combine(Directory.GetCurrentDirectory(), "testRootDir");
            string databasePath = Path.Combine(rootDir, "database");
            string exposurePath = Path.Combine(databasePath, "Exposure");

            const string initialBasin = "c-09";
            const string newBasinSelection = "c-10";
            string[] availableBasins = {initialBasin, newBasinSelection};
            if (Directory.Exists(rootDir))
                Directory.Delete(rootDir, true);
            foreach (string availableBasin in availableBasins)
            {
                Directory.CreateDirectory(Path.Combine(exposurePath, availableBasin));
            }

            // 2. Verify initial expectations.
            viewModel.SelectRootDirectory.Execute(rootDir);
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.Ready));
            Assert.That(viewModel.AvailableBasins, Is.Not.Empty);
            Assert.That(viewModel.SelectedBasin, Is.EqualTo(initialBasin));

            // 3. Define test action.
            TestDelegate testAction = () => viewModel.SelectedBasin = newBasinSelection;

            // 4. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(viewModel.SelectedBasin, Is.EqualTo(newBasinSelection));
            Assert.That(warningMssg, Is.Not.Null.Or.Empty);
        }
    }
}