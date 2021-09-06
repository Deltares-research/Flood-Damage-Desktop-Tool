using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
    }
}