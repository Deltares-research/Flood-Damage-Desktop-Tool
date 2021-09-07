using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FDT.Gui.Test
{
    public class GuiUtilsTest
    {

        public static IEnumerable InvalidSubdirectoryNames
        {
            get
            {
                yield return new TestCaseData(null).Returns(Enumerable.Empty<string>());
                yield return new TestCaseData(new List<string>().AsEnumerable()).Returns(Enumerable.Empty<string>());
            }
        }

        public static IEnumerable ValidSubDirectoryNames
        {
            get
            {
                yield return new TestCaseData(new List<string> {"Path"}).Returns("Path");
                yield return new TestCaseData(new List<string> {"Nested\\Path"}).Returns("Path");
                yield return new TestCaseData(new List<string> {$"Another\\Nested\\Path"}).Returns("Path");
                yield return new TestCaseData(new List<string> {$"Another\\Path\\"}).Returns("Path");
            }
        }

        // [Test]
        // [TestCaseSource(nameof(InvalidSubdirectoryNames))]
        // public object TestGetSubDirectoryNamesReturnsNothingWhenNoneGiven(IEnumerable<string> directories)
        // {
        //     IEnumerable<string> result = null;
        //     TestDelegate testAction = () => result = GuiUtils.GetSubDirectoryNames(directories?.ToArray()).ToArray();
        //     Assert.That(testAction, Throws.Nothing);
        //     return result;
        // }
        //
        // [Test]
        // [TestCaseSource(nameof(ValidSubDirectoryNames))]
        // public string TestGetSubDirectoryNameReturnsOnlyNamesWhenValidDirectoriesGiven(IEnumerable<string> foundDirectories)
        // {
        //     // 1. Define test data.
        //     string[] result = null;
        //
        //     // 2. Define test action.
        //     TestDelegate testAction = () => result = GuiUtils.GetSubDirectoryNames(foundDirectories.ToArray()).ToArray();
        //
        //     // 3. Verify final expectations.
        //     Assert.That(testAction, Throws.Nothing);
        //     // Assert.That(result, Is.EqualTo(new List<string>() { "Path" }));
        //     Assert.That(result.Count(), Is.EqualTo(1));
        //     return result.Single();
        // }
    }
}