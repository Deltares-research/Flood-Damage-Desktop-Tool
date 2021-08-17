using System;
using System.IO;
using FDT.Backend.ExeHandler;
using FDT.Backend.IExeHandler;
using NUnit.Framework;

namespace FDT.Backend.Test.ExeHandler
{
    public class FiatPythonWrapperTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test data
            string existingDirectory = Directory.GetCurrentDirectory();
            FiatPythonWrapper testWrapper = null;
            
            // Define test action
            TestDelegate testDelegate = () => testWrapper = new FiatPythonWrapper(existingDirectory);

            // Verify final expectations.
            Assert.That(testDelegate, Throws.Nothing);
            Assert.That(testWrapper, Is.Not.Null);
            Assert.That(testWrapper, Is.InstanceOf<IExeWrapper>());
            Assert.That(Path.GetFileName(testWrapper.ExeFilePath), Is.EqualTo("fiat_objects.exe"));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ConstructorThrowsExceptionWhenExeDirectoryNullOrEmpty(string exeDirectory)
        {
            TestDelegate testAction = () => new FiatPythonWrapper(exeDirectory);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("exeDirectory"));
        }

        [Test]
        public void ConstructorThrowsExceptionWhenExeDirectoryNotFound()
        {
            string notValidDir = "Not\\A\\Valid\\Path";
            TestDelegate testAction = () => new FiatPythonWrapper(notValidDir);
            Assert.That(testAction, Throws.Exception.TypeOf<DirectoryNotFoundException>());
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void RunThrowsArgumentNullExceptionWhenFilePathNullOrEmpty(string filePath)
        {
            string existingDirectory = Directory.GetCurrentDirectory();
            FiatPythonWrapper fiatPythonWrapper = new FiatPythonWrapper(existingDirectory);
            
            TestDelegate testAction = () => fiatPythonWrapper.Run(filePath);
            Assert.That(fiatPythonWrapper, Is.Not.Null);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("filePath"));
        }

        [Test]
        public void RunThrowsFileNotFoundExceptionWhenFilePathDoesNotExist()
        {
            const string testFilePath = "Non//Existing//Path";
            string existingDirectory = Directory.GetCurrentDirectory();
            FiatPythonWrapper fiatPythonWrapper = new FiatPythonWrapper(existingDirectory);

            TestDelegate testAction = () => fiatPythonWrapper.Run(testFilePath);
            Assert.That(fiatPythonWrapper, Is.Not.Null);
            Assert.That(testAction, Throws.Exception.TypeOf<FileNotFoundException>());
        }
    }
}