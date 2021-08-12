using System.ComponentModel;
using System.Linq;
using FDT.Gui.ViewModels;
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
        }
    }
}