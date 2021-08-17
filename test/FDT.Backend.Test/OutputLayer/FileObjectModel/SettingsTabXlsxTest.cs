using System;
using System.Linq;
using FDT.Backend.IDataModel;
using FDT.Backend.OutputLayer.FileObjectModel;
using FDT.Backend.OutputLayer.IFileObjectModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.OutputLayer.FileObjectModel
{
    public class SettingsTabXlsxTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test data
            SettingsTabXlsx testTab = null;
            const string scenarioName = "Dumb scenario";
            IBasin testBasin = Substitute.For<IBasin>();

            // Define test delegate
            TestDelegate testAction = () => testTab = new SettingsTabXlsx(testBasin, scenarioName);

            // Verify final expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(testTab, Is.Not.Null);
            Assert.That(testTab, Is.InstanceOf<ITabXlsx>());
            Assert.That(testTab.TabName, Is.EqualTo("Settings"));
            Assert.That(testTab.SettingsRowSingleEntry, Is.Not.Null);
            Assert.That(testTab.RowEntries.Single(), Is.EqualTo(testTab.SettingsRowSingleEntry));
        }

        [Test]
        public void ConstructorThrowsExceptionGivenNullBasin()
        {
            TestDelegate testAction = () => new SettingsTabXlsx(null, Arg.Any<string>());
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("basin"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void ConstructorThrowsExceptionGivenNotValidScenarioName(string scenarioName)
        {
            TestDelegate testAction = () => new SettingsTabXlsx(Substitute.For<IBasin>(), scenarioName);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("scenarioName"));
        }
    }
}