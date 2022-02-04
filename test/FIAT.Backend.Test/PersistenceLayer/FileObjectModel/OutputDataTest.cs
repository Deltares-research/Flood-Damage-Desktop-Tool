using System;
using FIAT.Backend.PersistenceLayer.FileObjectModel;
using FIAT.Backend.PersistenceLayer.IFileObjectModel;
using NUnit.Framework;

namespace FIAT.Backend.Test.PersistenceLayer.FileObjectModel
{
    public class OutputDataTest
    {
        [Test]
        public void ConstructorTest()
        {
            var outputDataTest = new OutputData();
            Assert.That(outputDataTest, Is.Not.Null);
            Assert.That(outputDataTest, Is.InstanceOf<IOutputData>());
        }

        [Test]
        [TestCaseSource(typeof(OutputDataTestData), nameof(OutputDataTestData.OutputDataInvalidParametersCases))]
        public void CheckValidParametersTestThrowsException(IOutputData outputData, Type exceptionType, string message)
        {
            TestDelegate testAction = () => outputData.CheckValidParameters();
            Assert.That(testAction, Throws.TypeOf(exceptionType).With.Message.Contains(message));
        }
    }
}