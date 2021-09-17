using System;
using System.IO;
using FDT.Backend.PersistenceLayer.IFileObjectModel;
using FDT.Backend.ServiceLayer.ExeHandler;
using FDT.Backend.ServiceLayer.IExeHandler;
using FDT.Backend.Test.PersistenceLayer.FileObjectModel;
using NUnit.Framework;

namespace FDT.Backend.Test.ServiceLayer.ExeHandler
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
            Assert.That(testAction,
                Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("outputData"));
        }

        [Test]
        [TestCaseSource(typeof(OutputDataTestData), nameof(OutputDataTestData.OutputDataInvalidParametersCases))]
        public void RunThrowsArgumentNullExceptionWhenFilePathNullOrEmpty(IOutputData outputData, Type exceptionType, string message)
        {
            TestDelegate testAction = () => new FiatPythonWrapper().Run(outputData);
            Assert.That(testAction,
                Throws.Exception.TypeOf(exceptionType).With.Message.Contains(message));
        }
    }
}