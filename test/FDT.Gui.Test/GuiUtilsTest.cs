using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        static object[] ValidSubDirectoryNames =
        {

            new object[] {new List<string> {"Path"}},
            new object[] {new List<string> { $"Nested{Path.DirectorySeparatorChar}Path"}},
            new object[] { new List<string> { $"Another{Path.DirectorySeparatorChar}Nested{Path.DirectorySeparatorChar}Path" }},
            new object[] { new List<string> { $"Another{Path.DirectorySeparatorChar}Path{Path.DirectorySeparatorChar}"}}
        };

        [Test]
        [TestCaseSource(nameof(InvalidSubdirectoryNames))]
        public object TestGetSubDirectoryNamesReturnsNothingWhenNoneGiven(IEnumerable<string> directories)
        {
            IEnumerable<string> result = null;
            TestDelegate testAction = () => result = GuiUtils.GetSubDirectoryNames(directories?.ToArray()).ToArray();
            Assert.That(testAction, Throws.Nothing);
            return result;
        }

        [Test]
        [TestCaseSource(nameof(ValidSubDirectoryNames))]
        public void TestGetSubDirectoryNameReturnsOnlyNamesWhenValidDirectoriesGiven(IEnumerable<string> foundDirectories)
        {
            // 1. Define test data.
            IEnumerable<string> result = null;

            // 2. Define test action.
            TestDelegate testAction = () => result = GuiUtils.GetSubDirectoryNames(foundDirectories.ToArray()).ToArray();

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(result, Is.EqualTo(new List<string>() { "Path" }));
        }
    }
}