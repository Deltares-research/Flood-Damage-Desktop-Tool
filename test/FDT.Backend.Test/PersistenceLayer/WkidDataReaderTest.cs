using System;
using System.IO;
using FDT.Backend.PersistenceLayer;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer
{
    public class WkidDataReaderTest
    {
        [Test]
        public void ConstructorTest()
        {
            var wkidDataReader = new WkidDataReader();
            Assert.That(wkidDataReader, Is.Not.Null);
            Assert.That(wkidDataReader, Is.InstanceOf<IReader>());
            Assert.That(wkidDataReader.BasinDir, Is.Null);
            Assert.That(() => wkidDataReader.FilePath, Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ReadInputDataThrowsExceptionWhenBasinDirIsNullOrEmpty(string basinDirPath)
        {
            TestDelegate testAction = () => new WkidDataReader() { BasinDir = basinDirPath }.ReadInputData();
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains(nameof(WkidDataReader.BasinDir)));
        }

        [Test]
        public void ReadInputDataThrowsExceptionWhenFilePathNotFound()
        {
            const string basinDirPath = "Not\\A\\Valid\\Path";
            TestDelegate testAction = () => new WkidDataReader(){BasinDir = basinDirPath}.ReadInputData();
            Assert.That(testAction, Throws.Exception.TypeOf<DirectoryNotFoundException>().With.Message.Contains(basinDirPath));
        }

        [Test]
        public void ReadInputDataReturnsTxtValueGivenValidBasinDirPath()
        {
            // 1. Prepare test data.
            const string wkidFileName = "WKID.txt";
            string wkidTestFile = Path.Combine(TestHelper.TestDatabaseDirectory, "exposure", "c-9", wkidFileName);
            Assert.That(File.Exists(wkidTestFile));
            string basinDirPath = Path.GetTempPath();
            string wkidCopyPath = Path.Combine(basinDirPath, wkidFileName);
            if(!File.Exists(wkidCopyPath))
                File.Copy(wkidTestFile, wkidCopyPath);
            Assert.That(File.Exists(wkidCopyPath));
            string wkidResult = string.Empty;
            
            // 2. Define test action.
            TestDelegate testAction = () => wkidResult = new WkidDataReader() { BasinDir = basinDirPath }.ReadInputData();

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(wkidResult, Is.Not.Null.Or.Empty);
            Assert.That(wkidResult, Is.EqualTo("4326"));
        }
    }
}