using System;
using System.Collections.Generic;
using System.Linq;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer.FileObjectModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class HazardTabXlsxTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test data.
            HazardTabXlsx tabTest = null;
            var basin = Substitute.For<IBasin>();
            var floodMap = Substitute.For<IFloodMapBase>();

            const string filePath = "DummyDataPath";
            const int returnObject = 42;

            basin.Projection.Returns("EPSG:42");
            floodMap.Path.Returns(filePath);
            floodMap.GetReturnPeriod().Returns(returnObject);

            // Run test.
            TestDelegate testAction = () => tabTest = new HazardTabXlsx(basin, new[] {floodMap});

            // Verify expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(tabTest, Is.Not.Null);
            Assert.That(tabTest, Is.InstanceOf<ITabXlsx>());
            Assert.That(tabTest.TabName, Is.EqualTo("Hazard"));
            Assert.That(tabTest.RowEntries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ConstructorGivenNullBasinThrowsException()
        {
            TestDelegate testAction = () => new HazardTabXlsx(null, new List<IFloodMapBase>());
            Assert.That(testAction, Throws.TypeOf<ArgumentNullException>().With.Message.Contains("basin"));
        }

        [Test]
        public void ConstructorGivenNullOrEmptyFloodMapsDoesNotThrow()
        {
            TestDelegate testAction = () => new HazardTabXlsx(Substitute.For<IBasin>(), null);
            Assert.That(testAction, Throws.TypeOf<ArgumentNullException>().With.Message.Contains("floodMaps"));
        }

        [Test]
        public void ConstructorRowsEntriesOnlyFromFloodMapsWithPath()
        {
            ITabXlsx createdTab = null;
            var basin = Substitute.For<IBasin>();
            var floodMapWithPath = Substitute.For<IFloodMapBase>();
            var floodMapWithoutPath = Substitute.For<IFloodMapBase>();

            basin.Projection.Returns("EPSG:42");
            floodMapWithPath.Path.Returns("DummyPath");

            // Define test delegate
            TestDelegate testAction = () => createdTab = new HazardTabXlsx(basin, new[] {floodMapWithPath, floodMapWithoutPath});

            // Verify final expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(createdTab, Is.Not.Null);
            Assert.That(createdTab.RowEntries.Count(), Is.EqualTo(1));
        }
        
    }
}