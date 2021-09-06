using FDT.Gui.Commands;
using FDT.Gui.ViewModels;
using NUnit.Framework;

namespace FDT.Gui.Test.Commands
{
    public class EnumExtensionsTest
    {
        [Test]
        [TestCaseSource(typeof(AssessmentStatusStringConverterTest), nameof(AssessmentStatusStringConverterTest.StringConverterCases))]
        public object TestGetDisplayName(AssessmentStatus value)
        {
            return EnumExtensions.GetDisplayName(value);
        }
    }
}