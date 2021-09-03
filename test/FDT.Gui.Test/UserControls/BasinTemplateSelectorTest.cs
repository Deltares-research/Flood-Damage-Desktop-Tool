using System.Windows;
using FDT.Gui.UserControls;
using FDT.Gui.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Gui.Test.UserControls
{
    public class BasinTemplateSelectorTest
    {
        [Test]
        public void TestConstructor()
        {
            var templateSelector = new BasinTemplateSelector();
            Assert.That(templateSelector, Is.Not.Null);
            Assert.That(templateSelector.EventTemplate, Is.Null);
            Assert.That(templateSelector.RiskTemplate, Is.Null);
        }

        [Test]
        public void TestGivenRiskBasedScenarioReturnsRiskTemplate()
        {
            var templateSelector = new BasinTemplateSelector();
            var riskBasedScenario = Substitute.For<RiskBasedScenario>();
            var riskTemplate = new DataTemplate(nameof(RiskBasedScenario));
            templateSelector.RiskTemplate = riskTemplate;
            DataTemplate returnTemplate = null;
            TestDelegate testAction = () => returnTemplate = templateSelector.SelectTemplate(riskBasedScenario, null);

            Assert.That(testAction, Throws.Nothing);
            Assert.That(returnTemplate, Is.EqualTo(riskTemplate));
        }

        [Test]
        public void TestGivenEventBasedScenarioReturnsRiskTemplate()
        {
            var templateSelector = new BasinTemplateSelector();
            var eventBasedScenario = Substitute.For<EventBasedScenario>();
            var eventTemplate = new DataTemplate(nameof(EventBasedScenario));
            templateSelector.EventTemplate = eventTemplate;
            DataTemplate returnTemplate = null;
            TestDelegate testAction = () => returnTemplate = templateSelector.SelectTemplate(eventBasedScenario, null);

            Assert.That(testAction, Throws.Nothing);
            Assert.That(returnTemplate, Is.EqualTo(eventTemplate));
        }
    }
}