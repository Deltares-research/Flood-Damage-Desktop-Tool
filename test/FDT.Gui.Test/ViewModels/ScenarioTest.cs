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
    }
}