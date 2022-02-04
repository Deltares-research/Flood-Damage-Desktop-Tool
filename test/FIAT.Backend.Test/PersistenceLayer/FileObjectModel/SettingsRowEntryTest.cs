using System;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Backend.PersistenceLayer.FileObjectModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class SettingsRowEntryTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test variables.
            SettingsRowEntry rowTest = null;
            const string scenarioName = "DumbName";

            IBasin basin = Substitute.For<IBasin>();
            basin.BasinName.Returns("Dummy Basin Name");
            basin.Projection.Returns("EPSG:42");
            
            // Define test delegate.
            TestDelegate testAction = () => rowTest = new SettingsRowEntry(basin, scenarioName);
            
            // Verify expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(rowTest, Is.Not.Null);
            Assert.That(rowTest, Is.InstanceOf<IRowEntry>());
            Assert.That(rowTest.GetOrderedColumns(null), Is.EqualTo(new []
            {
                basin.BasinName,
                scenarioName,
                basin.Projection,
                "feet"
            }));
        }

        [Test]
        public void ConstructorThrowsExceptionWithNullBasin()
        {
            TestDelegate testAction = () => new SettingsRowEntry(null, "dumbValidString");
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("basin"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ConstructorThrowsExceptionWithInvalidScenarioName(string scenarioName)
        {
            TestDelegate testAction = () => new SettingsRowEntry(Substitute.For<IBasin>(), scenarioName);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("scenarioName"));
        }
    }
}