using System;
using System.Linq;
using FDT.Gui.ViewModels;
using NUnit.Framework;

namespace FDT.Gui.Test.ViewModels
{
    public class BackendConverterTest
    {
        [Test]
        public void TestConvertBasinThrowsExceptionWithNullArguments()
        {
            TestDelegate testAction = () => BackendConverter.ConvertBasin(null, string.Empty);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("basinScenarios"));
        }

        [Test]
        public void TestConvertScenariosThrowsExceptionWithNullArguments()
        {
            TestDelegate testAction = () => BackendConverter.ConvertScenarios(null).ToArray();
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("scenarios"));
        }

        [Test]
        public void TestConvertFloodMapsThrowsExceptionWithNullArguments()
        {
            TestDelegate testAction = () => BackendConverter.ConvertFloodMaps(null).ToArray();
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("floodMaps"));
        }
    }
}