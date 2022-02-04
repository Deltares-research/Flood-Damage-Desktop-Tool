using System;
using System.Collections;
using System.Windows;
using FIAT.Gui.Commands;
using FIAT.Gui.ViewModels;
using NUnit.Framework;

namespace FIAT.Gui.Test.Commands
{
    public class AssessmentStatusVisibilityConverterTest
    {
        public static IEnumerable StringConverterCases
        {
            get
            {
                yield return new TestCaseData(AssessmentStatus.Ready).Returns(Visibility.Hidden);
                yield return new TestCaseData(AssessmentStatus.Running).Returns(Visibility.Hidden);
                yield return new TestCaseData(AssessmentStatus.LoadingBasins).Returns(Visibility.Visible);
            }
        }

        [Test]
        [TestCaseSource(nameof(StringConverterCases))]
        public object TestConvertFromEnumReturnsExpectedValue(AssessmentStatus status)
        {
            var converter = new AssessmentStatusVisibilityConverter();
            return converter.Convert(status, typeof(AssessmentStatus), null, null);
        }

        [Test]
        [TestCase(Visibility.Hidden)]
        [TestCase(Visibility.Collapsed)]
        [TestCase(Visibility.Visible)]
        public void TestConvertBackThrowsException(Visibility value)
        {
            TestDelegate testAction = () =>
                new AssessmentStatusVisibilityConverter().ConvertBack(value, typeof(Visibility), null, null);
            Assert.That(testAction, Throws.TypeOf<NotSupportedException>());
        }
    }
}