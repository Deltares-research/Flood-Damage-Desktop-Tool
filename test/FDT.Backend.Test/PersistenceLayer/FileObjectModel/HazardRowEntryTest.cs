using System;
using System.Linq;
using ClosedXML.Excel;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer.FileObjectModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class HazardRowEntryTest
    {
        static object[] GetFloodMapTypeCases =
        {
            new object[] { FloodMapType.WaterDepth, "DEM" },
            new object[] { FloodMapType.WaterLevel, "Datum" },
        };

        [Test]
        [TestCaseSource(nameof(GetFloodMapTypeCases))]
        public void ConstructorTest(FloodMapType mapType, string inundationRef)
        {
            // Define test data.
            HazardRowEntry hazardRowEntry = null;
            var floodMap = Substitute.For<IFloodMapBase>();
            var defaultRow = Substitute.For<IXLRow>();
            var returnCell = Substitute.For<IXLCell>();

            const string basinProjection = "EPSG:42";
            const string filePath = "DummyDataPath";
            const int returnObject = 42;
            
            floodMap.Path.Returns(filePath);
            floodMap.GetReturnPeriod().Returns(returnObject);
            floodMap.MapType.Returns(mapType);

            // Substitute expects these calls to be made, otherwise will fail.
            defaultRow.Cell(4).Returns(returnCell);

            // Generate object.
            TestDelegate testAction = () => hazardRowEntry = new HazardRowEntry(floodMap, basinProjection);

            // Verify expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(hazardRowEntry, Is.Not.Null);
            Assert.That(hazardRowEntry, Is.InstanceOf<IRowEntry>());
            Assert.That(hazardRowEntry.GetOrderedColumns(defaultRow), Is.EqualTo(new []
            {
                floodMap.Path,
                floodMap.GetReturnPeriod(),
                basinProjection,
                inundationRef,
            }));
        }

        [Test]
        public void ConstructorThrowsExceptionWithNullFloodMapBase()
        {
            TestDelegate testAction = () => new HazardRowEntry(null, "dumbValidString");
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("floodMapBase"));
        }

        [Test]
        public void GetOrderedColumnsThrowsArgumentNullExceptionWithNullRow()
        {
            // Define test data.
            var floodMap = Substitute.For<IFloodMapBase>();
            const string basinProjection = "EPSG:42";
            const string filePath = "DummyDataPath";
            const int returnObject = 42;

            floodMap.Path.Returns(filePath);
            floodMap.GetReturnPeriod().Returns(returnObject);

            // Verify initial expectations.
            var hazardRowEntry = new HazardRowEntry(floodMap, basinProjection);
            Assert.That(hazardRowEntry, Is.Not.Null);
            Assert.That(hazardRowEntry, Is.InstanceOf<IRowEntry>());

            // Generate object.
            TestDelegate testAction = () => hazardRowEntry.GetOrderedColumns(null);

            // Verify expectations.
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("defaultRow"));
        }

        [Test]
        public void GetOrderedColumnsThrowsExceptionWithInvalidValue()
        {
            // Define test data.
            var floodMap = Substitute.For<IFloodMapBase>();
            var defaultRow = Substitute.For<IXLRow>();

            const string basinProjection = "EPSG:42";
            const string filePath = "DummyDataPath";
            const int returnObject = 42;

            floodMap.Path.Returns(filePath);
            floodMap.GetReturnPeriod().Returns(returnObject);

            // Verify initial expectations.
            var hazardRowEntry = new HazardRowEntry(floodMap, basinProjection);
            Assert.That(hazardRowEntry, Is.Not.Null);
            Assert.That(hazardRowEntry, Is.InstanceOf<IRowEntry>());
            object[] expectedReturnValue = new[]
            {
                floodMap.Path,
                floodMap.GetReturnPeriod(),
                basinProjection,
                "DEM",
            };
            object[] returnValue = null;

            // Define test action.
            TestDelegate testAction = () => returnValue = hazardRowEntry.GetOrderedColumns(defaultRow).ToArray();

            // Verify expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(expectedReturnValue, Is.EqualTo(returnValue));
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