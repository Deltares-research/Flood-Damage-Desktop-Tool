using System;
using System.Collections;
using FIAT.Gui.Commands;
using FIAT.Gui.ViewModels;
using NUnit.Framework;

namespace FIAT.Gui.Test.Commands
{
    public class AssessmentStatusBoolConverterTest
    {
        public static IEnumerable BoolConverterCases
        {
            get
            {
                yield return new TestCaseData(AssessmentStatus.Ready).Returns(true);
                yield return new TestCaseData(AssessmentStatus.Running).Returns(false);
                yield return new TestCaseData(AssessmentStatus.LoadingBasins).Returns(false);
            }
        }

        [Test]
        [TestCaseSource(nameof(BoolConverterCases))]
        public object TestConvertFromEnumReturnsExpectedValue(AssessmentStatus status)
        {
            var converter = new AssessmentStatusBoolConverter();
            return converter.Convert(status, typeof(AssessmentStatus), null, null);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void TestConvertBackThrowsException(bool value)
        {
            TestDelegate testAction = () =>
                new AssessmentStatusBoolConverter().ConvertBack(value, typeof(bool), null, null);
            Assert.That(testAction, Throws.TypeOf<NotSupportedException>());
        }
    }
}