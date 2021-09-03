using System.Linq;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class ExposureTabXlsxTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test data
            IBasin basin = Substitute.For<IBasin>();
            basin.BasinName.Returns("SelectedBasinname");
            ExposureTabXlsx exposureTab = null;
            
            // Define test delegate
            TestDelegate testAction = () => exposureTab = new ExposureTabXlsx(basin);
            
            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(exposureTab, Is.Not.Null);
            Assert.That(exposureTab.TabName, Is.EqualTo("Exposure"));
            Assert.That(exposureTab.RowEntries.Count(), Is.EqualTo(1));
            Assert.That(exposureTab.RowEntries.Single(), Is.InstanceOf<ExposureRowEntry>());
            Assert.That(exposureTab.ExposureRowSingleEntry, Is.Not.Null);
            Assert.That(exposureTab.ExposureRowSingleEntry, Is.InstanceOf<IRowEntry>());
            Assert.That(exposureTab.RowEntries.Single(), Is.EqualTo(exposureTab.ExposureRowSingleEntry));
        }

        [Test]
        public void ConstructorThrowsException()
        {
            TestDelegate testAction = () => new ExposureTabXlsx(null);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains("basin"));
        }
    }
}