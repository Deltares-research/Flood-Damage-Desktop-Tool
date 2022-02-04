using FIAT.Backend.DomainLayer.DataModel;
using FIAT.Backend.DomainLayer.IDataModel;
using NUnit.Framework;

namespace FIAT.Backend.Test.DomainLayer.DataModel
{
    public class FloodMapBaseTest
    {
        [Test]
        public void FloodMapConstructorTest()
        {
            var floodMap = new FloodMap();
            Assert.That(floodMap, Is.Not.Null);
            Assert.That(floodMap, Is.InstanceOf<IFloodMapBase>());
            Assert.That(floodMap, Is.InstanceOf<IFloodMap>());
            Assert.That(floodMap.ReturnPeriod, Is.EqualTo("Event"));
            Assert.That(floodMap.GetReturnPeriod(), Is.TypeOf<string>());
        }

        [Test]
        public void FloodMapWithReturnPeriodConstructorTest()
        {
            var floodMap = new FloodMapWithReturnPeriod();
            Assert.That(floodMap, Is.Not.Null);
            Assert.That(floodMap, Is.InstanceOf<IFloodMapBase>());
            Assert.That(floodMap, Is.InstanceOf<IFloodMapWithReturnPeriod>());
            Assert.That(floodMap.ReturnPeriod, Is.EqualTo(default(int)));
            Assert.That(floodMap.GetReturnPeriod(), Is.TypeOf<int>());
        }
    }
}