using System;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class HazardRowEntryTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test data.
            HazardRowEntry hazardRowEntry = null;
            var floodMap = Substitute.For<IFloodMapBase>();
            
            const string basinProjection = "EPSG:42";
            const string filePath = "DummyDataPath";
            const int returnObject = 42;
            floodMap.Path.Returns(filePath);
            floodMap.GetReturnPeriod().Returns(returnObject);

            // Generate object.
            TestDelegate testAction = () => hazardRowEntry = new HazardRowEntry(floodMap, basinProjection);

            // Verify expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(hazardRowEntry, Is.Not.Null);
            Assert.That(hazardRowEntry, Is.InstanceOf<IRowEntry>());
            Assert.That(hazardRowEntry.GetOrderedColumns(), Is.EqualTo(new []
            {
                floodMap.Path,
                floodMap.GetReturnPeriod(),
                basinProjection,
            }));
        }

        [Test]
        public void ConstructorThrowsExceptionWithNullFloodMapBase()
        {
            TestDelegate testAction = () => new HazardRowEntry(null, "dumbValidString");
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("floodMapBase"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ConstructorThrowsExceptionWithInvalidBasinProjection(string basinProjection)
        {
            TestDelegate testAction = () => new HazardRowEntry(Substitute.For<IFloodMapBase>(), basinProjection);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("basinProjection"));
        }
    }
}