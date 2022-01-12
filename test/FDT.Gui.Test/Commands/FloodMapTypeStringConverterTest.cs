using System;
using System.Collections;
using FDT.Backend.DomainLayer.IDataModel;
using FDT.Gui.Commands;
using NUnit.Framework;

namespace FDT.Gui.Test.Commands
{
    public class FloodMapTypeStringConverterTest
    {
        public static IEnumerable StringConverterCases
        {
            get
            {
                yield return new TestCaseData(FloodMapType.WaterDepth).Returns("Water Depth");
                yield return new TestCaseData(FloodMapType.WaterLevel).Returns("Water Level");;
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