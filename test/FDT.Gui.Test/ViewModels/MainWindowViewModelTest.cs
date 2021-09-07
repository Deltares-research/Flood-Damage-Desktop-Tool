using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.Test;
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
            Assert.That(viewModel.BasinScenarios, Is.Not.Null.Or.Empty);
            Assert.That(viewModel.BasinScenarios.Count, Is.EqualTo(2));
            Assert.That(viewModel.BasinScenarios.Any( bs => bs.GetType() == typeof(EventBasedScenario)));
            Assert.That(viewModel.BasinScenarios.Any(bs => bs.GetType() == typeof(RiskBasedScenario)));
            Assert.That(viewModel.LoadBasins, Is.Not.Null);
            Assert.That(viewModel.RunDamageAssessment, Is.Not.Null);
            Assert.That(viewModel.BackendPaths, Is.Not.Null);
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.LoadingBasins));
            Assert.That(viewModel.AvailableBasins, Is.Empty);
        }

        public static IEnumerable InvalidExposurePath
        {
            get
            {
                yield return new TestCaseData(null, typeof(ArgumentNullException), "exposurePath");
                yield return new TestCaseData(string.Empty, typeof(ArgumentNullException), "exposurePath");
                yield return new TestCaseData("InvalidPath", typeof(DirectoryNotFoundException), "InvalidPath");
                yield return new TestCaseData("In\\valid\\Path", typeof(DirectoryNotFoundException), "In\\valid\\Path");
            }
        }

        [Test]
        [TestCaseSource(nameof(InvalidExposurePath))]
        public void TestOnLoadBasinsThrowsExceptionWhenInvalidArgument(string exposurePath, Type exceptionType, string exceptionMessage)
        {
            var viewModel = new MainWindowViewModel();
            TestDelegate testAction = () => viewModel.LoadBasins.Execute(exposurePath);
            Assert.That(testAction, Throws.TypeOf(exceptionType).With.Message.Contains(exceptionMessage));
        }

        [Test]
        public void TestOnLoadBasinsThrowsExceptionWhenNoSubDirectoriesFound()
        {
            // 1. Define test data.
            string exposurePath = Path.Combine(TestHelper.TestDatabaseDirectory, "invalid_exposure");
            string exceptionMessage = $"No basin subdirectories found at Exposure directory {exposurePath}";

            var viewModel =  new MainWindowViewModel();
            var backendPaths = Substitute.For<IApplicationPaths>();
            viewModel.BackendPaths = backendPaths;
            backendPaths.ExposurePath.Returns(exposurePath);
            backendPaths
                .When(bp => bp.UpdateExposurePath(exposurePath))
                .Do(bp => {});
            

            // 2. Define test action.
            TestDelegate testAction = () => viewModel.LoadBasins.Execute(exposurePath);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.TypeOf<Exception>().With.Message.Contains(exceptionMessage));
        }

        [Test]
        public void TestGivenValidExposurePathWhenLoadBasinsThenPathsAndBasinsAreUpdated()
        {
            // 1. Define test data.
            string exposurePath = Path.Combine(TestHelper.TestDatabaseDirectory, "exposure");

            var viewModel = new MainWindowViewModel();
            var backendPaths = Substitute.For<IApplicationPaths>();
            viewModel.BackendPaths = backendPaths;
            backendPaths.ExposurePath.Returns(exposurePath);

            // 2. Verify initial expectations.
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.LoadingBasins));
            Assert.That(viewModel.AvailableBasins, Is.Empty);
            Assert.That(viewModel.SelectedBasin, Is.Null);

            // 3. Define test action.
            TestDelegate testAction = () => viewModel.LoadBasins.Execute(exposurePath);

            // 4. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.Ready));
            Assert.That(viewModel.SelectedBasin, Is.Not.Null);
            // backendPaths.Received(1).UpdateExposurePath(Arg.Is<string>(x => x != string.Empty));
            // backendPaths.Received(1).UpdateSelectedBasin(Arg.Is<string>(x => x != string.Empty));
        }

        [Test]
        public void TestGivenInvalidRunPropertiesWhenRunDamageAssessmentThenRunStatusBackToReady()
        {
            var viewModel = new MainWindowViewModel();
            
            TestDelegate testAction = () => viewModel.RunDamageAssessment.Execute(null);
            
            // We just care that one exception is thrown.
            Assert.That(testAction, Throws.TypeOf<ArgumentNullException>());
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.Ready));
        }

        [Test]
        public void TestGivenValidRunPropertiesWhenRunDamageAssessmentThenRunStatusIsUpdated()
        {
            // 1. Define test data.
            var viewModel = new MainWindowViewModel();
            var statusTransition = new List<AssessmentStatus>();
            string exposurePath = Path.Combine(TestHelper.TestDatabaseDirectory, "exposure");

            // 2. Define expectations.
            viewModel.LoadBasins.Execute(exposurePath);
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName is nameof(MainWindowViewModel.RunStatus))
                {
                    statusTransition.Add(viewModel.RunStatus);
                }
            };
            
            // 3. Define test action.
            TestDelegate testAction = () => viewModel.RunDamageAssessment.Execute(null);

            // 4. Verify final expectations.
            // The test model is not entirely correct but it suffices to cover the logic on MainWindowViewModel.cs
            Assert.That(testAction, Throws.TypeOf<Exception>());
            Assert.That(viewModel.RunStatus, Is.EqualTo(AssessmentStatus.Ready));
            Assert.That(statusTransition.Count, Is.EqualTo(2));
            Assert.That(statusTransition[0], Is.EqualTo(AssessmentStatus.Running));
            Assert.That(statusTransition[1], Is.EqualTo(AssessmentStatus.Ready));
        }
        
    }
}