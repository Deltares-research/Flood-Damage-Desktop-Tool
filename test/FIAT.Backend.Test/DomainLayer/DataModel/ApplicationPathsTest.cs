using System.IO;
using System.Linq;
using FIAT.Backend.DomainLayer.DataModel;
using FIAT.Backend.DomainLayer.IDataModel;
using NUnit.Framework;

namespace FIAT.Backend.Test.DomainLayer.DataModel
{
    public class ApplicationPathsTest
    {
        [Test]
        public void ConstructorTest()
        {
            var applicationPaths = new ApplicationPaths();
            Assert.That(applicationPaths, Is.Not.Null);
            Assert.That(applicationPaths, Is.InstanceOf<IApplicationPaths>());
            Assert.That(applicationPaths.RootPath, Is.Not.Null.Or.Empty);
            Assert.That(applicationPaths.AvailableBasins, Is.Empty);
            Assert.That(applicationPaths.SelectedBasin, Is.Null);
        }

        [Test]
        public void ChangeRootDirectoryWithoutExposureDirThrowsDirectoryNotFoundException()
        {
            var exposurePath = Path.Combine("something", "database", "Exposure");
            string expectedErrorMssg = $"Exposure directory does not exist at {exposurePath}";
            TestDelegate testAction = () => new ApplicationPaths().ChangeRootDirectory("something");

            Assert.That(
                testAction, 
                Throws.Exception.TypeOf<DirectoryNotFoundException>().With.Message.Contains(expectedErrorMssg));
        }

        [Test]
        public void ChangeRootDirectoryWithoutExposureSubdirectoriesThrowsException()
        {
            // 1. Define test data.
            string currentDir = Directory.GetCurrentDirectory();
            string rootDir = Path.Combine(currentDir, "testRootDir");
            string databasePath = Path.Combine(rootDir, "database");
            string exposurePath = Path.Combine(databasePath, "Exposure");
            string exceptionMessage = $"No basin subdirectories found at Exposure directory {exposurePath}";

            if (Directory.Exists(exposurePath))
                Directory.Delete(exposurePath, true);
            Directory.CreateDirectory(exposurePath);

            // 2. Define test action.
            TestDelegate testAction = () => new ApplicationPaths().ChangeRootDirectory(rootDir);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Exception.With.Message.Contains(exceptionMessage));
        }

        [Test]
        public void ChangeRootDirectoryWithValidExposureDirUpdatesBasins()
        {
            // Define test data
            var applicationPaths = new ApplicationPaths();
            string currentDir = Directory.GetCurrentDirectory();
            string rootDir = Path.Combine(currentDir, "testRootDir");
            string databasePath = Path.Combine(rootDir, "database");
            string exposurePath = Path.Combine(databasePath, "Exposure");
            string[] exposureDirs = {"c42", "c24"};

            if (Directory.Exists(rootDir))
                Directory.Delete(rootDir, true);
            foreach (var exposureDir in exposureDirs)
            {
                Directory.CreateDirectory(Path.Combine(exposurePath, exposureDir));
            }

            // Define test action
            TestDelegate testAction = () => applicationPaths.ChangeRootDirectory(rootDir);

            // Verify final expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(applicationPaths.SelectedBasin, Is.Null);
            Assert.That(applicationPaths.AvailableBasins, Is.Not.Null);
            Assert.That(applicationPaths.AvailableBasins.Select(ab => ab.BasinName).OrderBy(ab => ab), Is.EqualTo(exposureDirs.OrderBy(e => e)));
        }

    }
}