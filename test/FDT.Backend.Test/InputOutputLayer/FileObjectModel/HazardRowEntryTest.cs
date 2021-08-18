using System;
using FDT.Backend.IDataModel;
using FDT.Backend.InputOutputLayer.FileObjectModel;
using FDT.Backend.InputOutputLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.InputOutputLayer.FileObjectModel
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
            Assert.That(hazardRowEntry.CRS, Is.EqualTo(basinProjection));
            Assert.That(hazardRowEntry.HazardFile, Is.EqualTo(filePath));
            Assert.That(hazardRowEntry.ReturnPeriod, Is.EqualTo(returnObject));
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