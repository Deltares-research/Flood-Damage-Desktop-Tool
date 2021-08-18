using System;
using System.IO;
using FDT.Backend.ExeHandler;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer.IFileObjectModel;
using NSubstitute;
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
        public void RunThrowsArgumentNullExceptionWhenOutputDataIsNull()
        {
            TestDelegate testAction = () => new FiatPythonWrapper().Run(null);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("outputData"));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void RunThrowsArgumentNullExceptionWhenFilePathNullOrEmpty(string filePath)
        {
            var outputData = Substitute.For<IOutputData>();
            outputData.FilePath.Returns(filePath);
            TestDelegate testAction = () => new FiatPythonWrapper().Run(outputData);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("FilePath"));
        }

        [Test]
        public void RunThrowsFileNotFoundExceptionWhenFilePathDoesNotExist()
        {
            const string testFilePath = "Non//Existing//Path";
            var outputData = Substitute.For<IOutputData>();
            outputData.FilePath.Returns(testFilePath);
            TestDelegate testAction = () => new FiatPythonWrapper().Run(outputData);
            Assert.That(testAction, Throws.Exception.TypeOf<FileNotFoundException>());
        }
    }
}