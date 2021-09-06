using System;
using System.Collections;
using FDT.Gui.Commands;
using FDT.Gui.ViewModels;
using NUnit.Framework;

namespace FDT.Gui.Test.Commands
{
    public class AssessmentStatusStringConverterTest
    {
        public static IEnumerable StringConverterCases
        {
            get
            {
                yield return new TestCaseData(AssessmentStatus.Ready).Returns("Run Damage Assessment");
                yield return new TestCaseData(AssessmentStatus.Running).Returns("Running (wait please)");
                yield return new TestCaseData(AssessmentStatus.LoadingBasins).Returns("Cannot run (no basins)");
            }
        }

        [Test]
        [TestCaseSource(nameof(StringConverterCases))]
        public object TestConvertFromEnumReturnsExpectedValue(AssessmentStatus status)
        {
            var converter = new AssessmentStatusStringConverter();
            return converter.Convert(status, typeof(AssessmentStatus), null, null);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TestConvertBackThrowsException(string value)
        {
            TestDelegate testAction = () =>
                new AssessmentStatusStringConverter().ConvertBack(value, typeof(string), null, null);
            Assert.That(testAction, Throws.TypeOf<NotSupportedException>());
        }
    }
}