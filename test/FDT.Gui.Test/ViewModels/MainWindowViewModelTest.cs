using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Gui.ViewModels;
using NSubstitute;
using NUnit.Framework;


namespace FDT.Gui.Test.ViewModels
{
    [TestFixture]
    public class MainWindowViewModelTest
    {
        [Test]
        public void Test_Constructor()
        {
            var viewModel = new MainWindowViewModel();
            Assert.That(viewModel, Is.InstanceOf<INotifyPropertyChanged>());
            Assert.That(viewModel.BasinScenarios, Is.Empty);
            Assert.That(viewModel.SelectRootDirectory, Is.Not.Null);
            Assert.That(viewModel.RunDamageAssessment, Is.Not.Null);
            Assert.That(viewModel.BackendPaths, Is.Not.Null);
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.LoadingBasins));
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
            string currentDir = Directory.GetCurrentDirectory();
            string rootDir = Path.Combine(currentDir, "testRootDir");
            string exceptionMessage = $"No basin subdirectories found at Exposure directory {rootDir}";

            if (Directory.Exists(rootDir))
                Directory.Delete(rootDir, true);
            Directory.CreateDirectory(rootDir);

            var viewModel =  new MainWindowViewModel();
            var backendPaths = Substitute.For<IApplicationPaths>();
            viewModel.BackendPaths = backendPaths;
            backendPaths.ExposurePath.Returns(rootDir);

            // 2. Define test action.
            TestDelegate testAction = () => viewModel.SelectRootDirectory.Execute(rootDir);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.TypeOf<Exception>().With.Message.Contains(exceptionMessage));
        }

        [Test]
        public void TestGivenValidExposurePathWhenSelectRootDirectoryThenPathsAndBasinsAreUpdated()
        {
            // 1. Define test data.
            string rootDir = Path.Combine(Directory.GetCurrentDirectory(), "testRootDir");
            string exposurePath = Path.Combine(rootDir, "exposure");
            string basinPath = Path.Combine(exposurePath, "c-9");
            if(Directory.Exists(rootDir))
                Directory.Delete(rootDir, true);
            Directory.CreateDirectory(basinPath);

            var viewModel = new MainWindowViewModel();
            var backendPaths = Substitute.For<IApplicationPaths>();
            viewModel.BackendPaths = backendPaths;
            backendPaths.ExposurePath.Returns(exposurePath);

            // 2. Verify initial expectations.
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.LoadingBasins));
            Assert.That(viewModel.AvailableBasins, Is.Empty);
            Assert.That(viewModel.SelectedBasin, Is.Null);

            // 3. Define test action.
            TestDelegate testAction = () => viewModel.SelectRootDirectory.Execute(exposurePath);

            // 4. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.Ready));
            Assert.That(viewModel.SelectedBasin, Is.Not.Null);
        }
    }
}