using FDT.Gui.ViewModels;
using NUnit.Framework;

namespace FDT.Gui.Test.ViewModels
{
    public class BaseFloodMapTest
    {
        [Test]
        public void TestFloodMapHasValidReturnPeriod()
        {
            var floodMap = new FloodMap();
            Assert.That(floodMap, Is.Not.Null);
            Assert.That(floodMap, Is.InstanceOf<IFloodMap>());
            Assert.That(floodMap.HasReturnPeriod, Is.False);
            Assert.That(floodMap[nameof(floodMap.ReturnPeriod)], Is.EqualTo(null));
        }

        [Test]
        [TestCase(0, "Return Period should be greater than 0")]
        [TestCase(42, null)]
        public void TestFloodMapWithReturnPeriodValidationWorks(int returnPeriod, string error)
        {
            var floodMap = new FloodMapWithReturnPeriod();
            floodMap.ReturnPeriod = returnPeriod;
            Assert.That(floodMap, Is.Not.Null);
            Assert.That(floodMap, Is.InstanceOf<IFloodMap>());
            Assert.That(floodMap.HasReturnPeriod, Is.True);
            Assert.That(floodMap[nameof(floodMap.ReturnPeriod)], Is.EqualTo(error));
        }
    }
}