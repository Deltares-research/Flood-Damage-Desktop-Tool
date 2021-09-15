using System.IO;
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
            const string exposureDir = "Exposure";
            const string exposureCsv = "exposure.csv";

            var exposureRowEntry = new ExposureRowEntry(selectedBasin, exposureDir);
            Assert.That(exposureRowEntry, Is.Not.Null);
            Assert.That(exposureRowEntry, Is.InstanceOf<IRowEntry>());
            Assert.That(exposureRowEntry.GetOrderedColumns(null), Is.EqualTo(new []
            {
                Path.Combine(exposureDir, selectedBasin, exposureCsv),
                string.Empty,
            }));
        }

        static object[] InvalidStringCases =
        {
            new object[] { null },
            new object[] { string.Empty}
        };

        [Test]
        [TestCaseSource(nameof(InvalidStringCases))]
        public void ConstructorThrowsExceptionWithNullOrEmptySelectedBasin(string selectedBasin)
        {
            TestDelegate testAction = () => new ExposureRowEntry(selectedBasin, null);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains(nameof(selectedBasin)));
        }

        [Test]
        [TestCaseSource(nameof(InvalidStringCases))]
        public void ConstructorThrowsExceptionWithNullOrEmptyExposureDir(string exposurePath)
        {
            const string validBasin = "AnyBasin";
            TestDelegate testAction = () => new ExposureRowEntry(validBasin, null);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains(nameof(exposurePath)));
        }
    }
}