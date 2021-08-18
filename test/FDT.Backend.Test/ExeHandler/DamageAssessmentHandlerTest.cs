using System;
using System.Collections.Generic;
using System.Linq;
using FDT.Backend.ExeHandler;
using FDT.Backend.IDataModel;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.ExeHandler
{
    public class DamageAssessmentHandlerTest
    {
        [Test]
        public void ConstructorTest()
        {
            // Define test variables.
            DamageAssessmentHandler testHandler = null;
            IFloodDamageDomain dummyDomain = Substitute.For<IFloodDamageDomain>();
            dummyDomain.Paths.SystemPath.Returns(Environment.CurrentDirectory);

            // Define test action.
            TestDelegate testAction = () => testHandler = new DamageAssessmentHandler();
            
            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(testHandler, Is.Not.Null);
            Assert.That(testHandler, Is.InstanceOf<IRunnerHandler>());
            Assert.That(testHandler.DataDomain, Is.Null);
            Assert.That(testHandler.ExeWrapper, Is.Not.Null);
            Assert.That(testHandler.ExeWrapper, Is.InstanceOf<FiatPythonWrapper>());
            Assert.That(testHandler.ExeWrapper, Is.InstanceOf<IExeWrapper>());
            Assert.That(testHandler.DataWriter, Is.Not.Null);
            Assert.That(testHandler.DataWriter, Is.InstanceOf<XlsxDataWriter>());
            Assert.That(testHandler.DataWriter, Is.InstanceOf<IWriter>());

        }

        [Test]
        public void GivenMultipleAssessmentFilesDoesMultipleExeRuns()
        {
            // Set up test data.
            var damageAssessmentHandler = Substitute.ForPartsOf<DamageAssessmentHandler>();
            IExeWrapper exeWrapper = Substitute.For<IExeWrapper>();
            IWriter dataWriter = Substitute.For<IWriter>();
            IFloodDamageDomain dataDomain = Substitute.For<IFloodDamageDomain>();
            string[] testFilePaths = 
            {
                "path\\one", "path\\two", "path\\three"
            };

            damageAssessmentHandler.ExeWrapper.Returns(exeWrapper);
            damageAssessmentHandler.DataWriter.Returns(dataWriter);
            damageAssessmentHandler.DataDomain.Returns(dataDomain);
            dataWriter
                .WriteData(Arg.Any<IFloodDamageDomain>())
                .Returns(testFilePaths.AsEnumerable());

            // Define test actions.
            TestDelegate testAction = () => damageAssessmentHandler.Run();

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            foreach (string testFilePath in testFilePaths)
            {
                // NSubstitute assert call was received with given argument.
                exeWrapper.Received().Run(testFilePath);
            }
        }
    }
}