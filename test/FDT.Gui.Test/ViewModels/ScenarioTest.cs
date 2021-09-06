using System.Linq;
using FDT.Gui.ViewModels;
using NUnit.Framework;

namespace FDT.Gui.Test.ViewModels
{
    public class ScenarioTest
    {
        [Test]
        public void Test_ScenarioConstructor_ForFloodMap()
        {
            var model = new Scenario<FloodMap>();
            Assert.That(model, Is.InstanceOf<IScenario>());
            Assert.That(model.FloodMaps, Is.Not.Null.Or.Empty);
            Assert.That(model.FloodMaps.Count, Is.EqualTo(1));
            Assert.That(model.FloodMaps.All(fm => fm.GetType() == typeof(FloodMap)));
        }

        [Test]
        public void Test_Scenario_AddExtraFloodMap_ForFloodMap()
        {
            var model = new Scenario<FloodMap>();
            TestDelegate testAction = () => model.AddExtraFloodMap();
            Assert.That(testAction, Throws.Nothing);
            Assert.That(model.FloodMaps.Count, Is.EqualTo(2));
            Assert.That(model.FloodMaps.All(fm => fm.GetType() == typeof(FloodMap)));
        }

        [Test]
        public void Test_ScenarioConstructor_ForFloodMapWithReturnPeriod()
        {
            var model = new Scenario<FloodMapWithReturnPeriod>();
            Assert.That(model, Is.InstanceOf<IScenario>());
            Assert.That(model.FloodMaps, Is.Not.Null.Or.Empty);
            Assert.That(model.FloodMaps.Count, Is.EqualTo(1));
            Assert.That(model.FloodMaps.All(fm => fm.GetType() == typeof(FloodMapWithReturnPeriod)));
        }

        [Test]
        public void Test_Scenario_AddExtraFloodMap_ForFloodMapWithReturnPeriod()
        {
            var model = new Scenario<FloodMapWithReturnPeriod>();
            TestDelegate testAction = () => model.AddExtraFloodMap();
            Assert.That(testAction, Throws.Nothing);
            Assert.That(model.FloodMaps.Count, Is.EqualTo(2));
            Assert.That(model.FloodMaps.All(fm => fm.GetType() == typeof(FloodMapWithReturnPeriod)));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Test_Scenario_InvalidName_ShowsError(string scenarioName)
        {
            // It does not really matter which type of scenario.
            var model = new Scenario<FloodMap>();
            TestDelegate testAction = () => model.ScenarioName = scenarioName;
            Assert.That(testAction, Throws.Nothing);
            Assert.That(model[nameof(model.ScenarioName)], Is.EqualTo("Scenario Name is required"));
        }

        [Test]
        public void Test_Scenario_ValidName_ShowsNoError()
        {
            var model = new Scenario<FloodMap>();
            TestDelegate testAction = () => model.ScenarioName = "A Name";
            Assert.That(testAction, Throws.Nothing);
            Assert.That(model[nameof(model.ScenarioName)], Is.Null);
        }
    }
}