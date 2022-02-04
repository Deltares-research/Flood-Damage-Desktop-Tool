using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FIAT.Backend.DomainLayer.IDataModel;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Backend.Test
{
    public class BackendUtilsTest
    {
        [Test]
        public void TestConvertBasinGivenInvalidBasin()
        {
            TestDelegate testAction = () => BackendUtils.ConvertBasin(null, new List<IScenario>());
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains("basinData"));
        }

        [Test]
        public void TestConvertBasinGivenInvalidScenarios()
        {
            TestDelegate testAction = () => BackendUtils.ConvertBasin(Substitute.For<IBasin>(), null);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains("scenarios"));
        }

        [Test]
        public void TestConvertBasinGivenValidArguments()
        {
            // Define test data
            var testBasin = Substitute.For<IBasin>();
            var scenario = Substitute.For<IScenario>();
            IFloodDamageBasin resultBasin = null;
            testBasin.Projection.Returns("aProjection");
            testBasin.BasinName.Returns("aBasinName");

            // Define test action
            TestDelegate testAction = () => resultBasin = BackendUtils.ConvertBasin(testBasin, new[] {scenario});

            // Verify final expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(resultBasin, Is.Not.Null);
            Assert.That(resultBasin.Scenarios.Contains(scenario));
            Assert.That(resultBasin.Projection, Is.EqualTo(testBasin.Projection));
            Assert.That(resultBasin.BasinName, Is.EqualTo(testBasin.BasinName));
        }

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

        [Test]
        [TestCaseSource(nameof(InvalidSubdirectoryNames))]
        public object TestGetSubDirectoryNamesReturnsNothingWhenNoneGiven(IEnumerable<string> directories)
        {
            IEnumerable<string> result = null;
            TestDelegate testAction = () => result = BackendUtils.GetSubDirectoryNames(directories?.ToArray()).ToArray();
            Assert.That(testAction, Throws.Nothing);
            return result;
        }
        
        [Test]
        [TestCaseSource(nameof(ValidSubDirectoryNames))]
        public string TestGetSubDirectoryNameReturnsOnlyNamesWhenValidDirectoriesGiven(IEnumerable<string> foundDirectories)
        {
            // 1. Define test data.
            string[] result = null;
        
            // 2. Define test action.
            TestDelegate testAction = () => result = BackendUtils.GetSubDirectoryNames(foundDirectories.ToArray()).ToArray();
        
            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            // Assert.That(result, Is.EqualTo(new List<string>() { "Path" }));
            Assert.That(result.Count(), Is.EqualTo(1));
            return result.Single();
        }
    }
}