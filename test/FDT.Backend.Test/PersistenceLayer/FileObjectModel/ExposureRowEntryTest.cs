using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class ExposureRowEntryTest
    {
        [Test]
        public void ConstructorTest()
        {
            const string selectedBasin = "AnyBasin";

            var exposureRowEntry = new ExposureRowEntry(selectedBasin);
            Assert.That(exposureRowEntry, Is.Not.Null);
            Assert.That(exposureRowEntry, Is.InstanceOf<IRowEntry>());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ConstructorThrowsExceptionWithNullOrEmptySelectedBasin(string selectedBasin)
        {
            TestDelegate testAction = () => new ExposureRowEntry(selectedBasin);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains("selectedBasin"));
        }
    }
}