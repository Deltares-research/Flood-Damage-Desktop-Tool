using System;
using System.Collections;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Gui.Commands;
using NUnit.Framework;

namespace FIAT.Gui.Test.Commands
{
    public class FloodMapTypeStringConverterTest
    {
        public static IEnumerable StringConverterCases
        {
            get
            {
                yield return new TestCaseData(FloodMapType.WaterDepth).Returns("Water depth");
                yield return new TestCaseData(FloodMapType.WaterLevel).Returns("Water level");;
            }
        }

        [Test]
        [TestCaseSource(nameof(StringConverterCases))]
        public object TestConvertFromEnumReturnsExpectedValue(FloodMapType status)
        {
            var converter = new FloodMapTypeStringConverter();
            return converter.Convert(status, typeof(FloodMapType), null, null);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TestConvertBackThrowsException(string value)
        {
            TestDelegate testAction = () =>
                new FloodMapTypeStringConverter().ConvertBack(value, typeof(string), null, null);
            Assert.That(testAction, Throws.TypeOf<NotSupportedException>());
        }
    }
}