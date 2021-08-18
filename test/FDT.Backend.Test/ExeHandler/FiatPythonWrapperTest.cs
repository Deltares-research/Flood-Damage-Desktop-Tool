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
            FiatPythonWrapper testWrapper = null;
            
            // Define test action
            TestDelegate testDelegate = () => testWrapper = new FiatPythonWrapper();

            // Verify final expectations.
            Assert.That(testDelegate, Throws.Nothing);
            Assert.That(testWrapper, Is.Not.Null);
            Assert.That(testWrapper, Is.InstanceOf<IExeWrapper>());
            Assert.That(testWrapper.ExeDirectory, Is.Not.Null.Or.Empty);
            Assert.That(Path.GetFileName(testWrapper.ExeFilePath), Is.EqualTo("fiat_objects.exe"));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void RunThrowsArgumentNullExceptionWhenFilePathNullOrEmpty(string filePath)
        {
            TestDelegate testAction = () => new FiatPythonWrapper().Run(filePath);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("filePath"));
        }

        [Test]
        public void RunThrowsFileNotFoundExceptionWhenFilePathDoesNotExist()
        {
            const string testFilePath = "Non//Existing//Path";
            TestDelegate testAction = () => new FiatPythonWrapper().Run(testFilePath);
            Assert.That(testAction, Throws.Exception.TypeOf<FileNotFoundException>());
        }
    }
}