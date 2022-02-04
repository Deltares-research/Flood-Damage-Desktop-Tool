using System;
using FIAT.Backend.DomainLayer.DataModel;
using FIAT.Backend.DomainLayer.IDataModel;
using NUnit.Framework;

namespace FIAT.Backend.Test.DomainLayer.DataModel
{
    public class BasinUtilsTest
    {
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TestGetBasinWithInvalidArgumentThrowsException(string selectedBasinPath)
        {
            TestDelegate testAction = () => BasinUtils.GetBasin(selectedBasinPath);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("selectedBasinPath"));
        }

        [Test]
        public void TestGetBasinWithNonExistingFileReturnsValue()
        {
            IBasin resultBasin = null;
            const string basinPath = "rootValue\\aValue";
            TestDelegate testAction = () => resultBasin = BasinUtils.GetBasin(basinPath);
            Assert.That(testAction, Throws.Nothing);
            Assert.That(resultBasin, Is.Not.Null);
            Assert.That(resultBasin.Projection, Is.EqualTo(string.Empty));
            Assert.That(resultBasin.BasinName, Is.EqualTo("aValue"));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TestGetWidValueWithInvalidArgumentThrowsException(string selectedBasinPath)
        {
            TestDelegate testAction = () => BasinUtils.GetWkidValue(selectedBasinPath);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("selectedBasinPath"));
        }

        [Test]
        public void TestGetWkidValueWithNonExistingFileReturnsEmptyString()
        {
            string wkidValue = null;
            TestDelegate testAction = () => wkidValue = BasinUtils.GetWkidValue("aValue");
            Assert.That(testAction, Throws.Nothing);
            Assert.That(wkidValue, Is.EqualTo(string.Empty));
        }
    }
}