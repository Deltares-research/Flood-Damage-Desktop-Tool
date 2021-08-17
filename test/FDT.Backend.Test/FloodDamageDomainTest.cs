using FDT.Backend.IDataModel;
using NUnit.Framework;

namespace FDT.Backend.Test
{
    public class FloodDamageDomainTest
    {
        [Test]
        public void TestConstructor()
        {
            var testDomain = new FloodDamageDomain();
            Assert.That(testDomain, Is.Not.Null);
            Assert.That(testDomain, Is.InstanceOf<IFloodDamageDomain>());
            Assert.That(testDomain.BasinData, Is.Not.Null);
            Assert.That(testDomain.BasinData, Is.InstanceOf<IBasin>());
            Assert.That(testDomain.Paths, Is.Not.Null);
            Assert.That(testDomain.Paths, Is.InstanceOf<IApplicationPaths>());
        }
    }
}