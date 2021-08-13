using System.Linq;
using FDT.Gui.ViewModels;
using NUnit.Framework;

namespace FDT.Gui.Test.ViewModels
{
    public class BasinScenarioTest
    {
        [Test]
        public void Test_EventBasedScenarioConstructor()
        {
            var model = new EventBasedScenario();
            Assert.That(model, Is.InstanceOf<IBasinScenario>());
            Assert.That(model.ScenarioType, Is.EqualTo("Event"));
            Assert.That(model.Scenarios, Is.Not.Null.Or.Empty);
            Assert.That(model.Scenarios.ToArray().All(fm => fm.GetType() == typeof(Scenario<FloodMap>)));
        }

        [Test]
        public void Test_EventScenario_AddExtraFloodMap_ForFloodMap()
        {
            var model = new EventBasedScenario();
            TestDelegate testAction = () => model.AddExtraScenario();
            Assert.That(testAction, Throws.Nothing);
            Assert.That(model.Scenarios.Count, Is.EqualTo(2));
            Assert.That(model.Scenarios.ToArray().All(fm => fm.GetType() == typeof(Scenario<FloodMap>)));
        }

        [Test]
        public void Test_RiskBasedScenarioConstructor()
        {
            var model = new RiskBasedScenario();
            Assert.That(model, Is.InstanceOf<IBasinScenario>());
            Assert.That(model.ScenarioType, Is.EqualTo("Risk"));
            Assert.That(model.Scenarios, Is.Not.Null.Or.Empty);
            Assert.That(model.Scenarios.ToArray().All(fm => fm.GetType() == typeof(Scenario<FloodMapWithReturnPeriod>)));
        }

        [Test]
        public void Test_RiskScenario_AddExtraFloodMap_ForFloodMap()
        {
            var model = new RiskBasedScenario();
            TestDelegate testAction = () => model.AddExtraScenario();
            Assert.That(testAction, Throws.Nothing);
            Assert.That(model.Scenarios.Count, Is.EqualTo(2));
            Assert.That(model.Scenarios.ToArray().All(fm => fm.GetType() == typeof(Scenario<FloodMapWithReturnPeriod>)));
        }


    }
}