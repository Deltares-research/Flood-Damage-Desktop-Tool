using FDT.Backend.InputOutputLayer.FileObjectModel;
using FDT.Backend.InputOutputLayer.IFileObjectModel;
using NUnit.Framework;

namespace FDT.Backend.Test.InputOutputLayer.FileObjectModel
{
    public class ExposureRowEntryTest
    {
        [Test]
        public void ConstructorTest()
        {
            const string selectedBasin = "AnyBasin";
            string expectedExposureRelativePath =  $"Exposure\\{selectedBasin}\\{ExposureRowEntry.ExposureFileName}";

            var exposureRowEntry = new ExposureRowEntry(selectedBasin);
            Assert.That(exposureRowEntry, Is.Not.Null);
            Assert.That(exposureRowEntry, Is.InstanceOf<IRowEntry>());
            Assert.That(exposureRowEntry.ExposureRelativePath, Is.EqualTo(expectedExposureRelativePath));
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