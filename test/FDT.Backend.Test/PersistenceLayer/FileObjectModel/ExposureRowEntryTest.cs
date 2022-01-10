using System.IO;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Backend.PersistenceLayer.FileObjectModel;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class ExposureRowEntryTest
    {
        [Test]
        public void ConstructorTest()
        {
            IBasin selectedBasin = Substitute.For<IBasin>();
            const string selectedBasinName = "AnyBasin";
            const string selectedBasinProj = "AnyProjection";
            const string exposureDir = "Exposure";
            const string exposureCsv = "exposure.csv";
            selectedBasin.BasinName.Returns(selectedBasinName);
            selectedBasin.Projection.Returns(selectedBasinProj);

            var exposureRowEntry = new ExposureRowEntry(selectedBasin, exposureDir);
            Assert.That(exposureRowEntry, Is.Not.Null);
            Assert.That(exposureRowEntry, Is.InstanceOf<IRowEntry>());
            Assert.That(exposureRowEntry.GetOrderedColumns(null), Is.EqualTo(new []
            {
                Path.Combine(exposureDir, selectedBasinName, exposureCsv),
                selectedBasinProj,
            }));
        }

        static object[] InvalidStringCases =
        {
            new object[] { null },
            new object[] { string.Empty}
        };

        [Test]
        public void ConstructorThrowsExceptionWithNullOrEmptySelectedBasin()
        {
            TestDelegate testAction = () => new ExposureRowEntry(null, null);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains("selectedBasin"));
        }

        [Test]
        [TestCaseSource(nameof(InvalidStringCases))]
        public void ConstructorThrowsExceptionWithNullOrEmptySelectedBasinName(string selectedBasinName)
        {
            IBasin selectedBasin = Substitute.For<IBasin>();
            selectedBasin.BasinName.Returns(selectedBasinName);
            TestDelegate testAction = () => new ExposureRowEntry(selectedBasin, null);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains(nameof(IBasin.BasinName)));
        }

        [Test]
        [TestCaseSource(nameof(InvalidStringCases))]
        public void ConstructorThrowsExceptionWithNullOrEmptySelectedBasinProjection(string selectedBasinProjection)
        {
            IBasin selectedBasin = Substitute.For<IBasin>();
            selectedBasin.BasinName.Returns("AnyBasin");
            selectedBasin.Projection.Returns(selectedBasinProjection);
            TestDelegate testAction = () => new ExposureRowEntry(selectedBasin, null);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains(nameof(IBasin.Projection)));
        }

        [Test]
        [TestCaseSource(nameof(InvalidStringCases))]
        public void ConstructorThrowsExceptionWithNullOrEmptyExposureDir(string exposurePath)
        {
            IBasin selectedBasin = Substitute.For<IBasin>();
            selectedBasin.BasinName.Returns("AnyBasin");
            selectedBasin.Projection.Returns("AProjection");
            TestDelegate testAction = () => new ExposureRowEntry(selectedBasin, exposurePath);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains(nameof(exposurePath)));
        }
    }
}