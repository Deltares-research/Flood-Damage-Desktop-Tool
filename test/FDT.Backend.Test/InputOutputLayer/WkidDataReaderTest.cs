using System;
using System.IO;
using FDT.Backend.InputOutputLayer;
using NUnit.Framework;

namespace FDT.Backend.Test.InputOutputLayer
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
        public void ReadInputDataThrowsExceptionWhenBasinDirIsNull()
        {
            TestDelegate testAction = () => new WkidDataReader().ReadInputData();
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains(nameof(WkidDataReader.BasinDir)));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void GetWkidCodeThrowsExceptionWhenBasinDirIsNullOrEmpty(string basinDir)
        {
            TestDelegate testAction = () => new WkidDataReader().GetWkidCode(basinDir);
            Assert.That(testAction, Throws.ArgumentNullException.With.Message.Contains("basinDir"));
        }

        [Test]
        public void GetWkidCodeThrowsExceptionWhenFilePathNotFound()
        {
            const string basinDirPath = "Not\\A\\Valid\\Path";
            TestDelegate testAction = () => new WkidDataReader().GetWkidCode(basinDirPath);
            Assert.That(testAction, Throws.Exception.TypeOf<DirectoryNotFoundException>().With.Message.Contains(basinDirPath));
        }

        [Test]
        public void GetWkidCodeReturnsTxtValueGivenValidBasinDirPath()
        {
            // 1. Prepare test data.
            var wkidTestFile = Path.Combine(TestHelper.TestRootDirectory,"database", "exposure", "c-9", WkidDataReader.WkidFileName);
            Assert.That(File.Exists(wkidTestFile));
            string basinDirPath = Path.GetTempPath();
            var wkidCopyPath = Path.Combine(basinDirPath, WkidDataReader.WkidFileName);
            File.Copy(wkidTestFile, wkidCopyPath);
            string wkidResult = string.Empty;
            
            // 2. Define test action.
            TestDelegate testAction = () => wkidResult = new WkidDataReader().GetWkidCode(basinDirPath);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(wkidResult, Is.Not.Null.Or.Empty);
            Assert.That(wkidResult, Is.EqualTo("4326"));
        }
    }
}