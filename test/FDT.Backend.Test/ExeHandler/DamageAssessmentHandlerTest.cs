using System;
using System.Collections.Generic;
using System.Linq;
using FDT.Backend.ExeHandler;
using FDT.Backend.IDataModel;
using FDT.Backend.IExeHandler;
using FDT.Backend.OutputLayer;
using FDT.Backend.OutputLayer.IFileObjectModel;
using NSubstitute;
using NSubstitute.Extensions;
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
        public void GivenValidateRunFailsThrowsException()
        {
            IOutputData outputData = Substitute.For<IOutputData>();
            outputData.BasinName.Returns("TestBasin");
            outputData.ScenarioName.Returns("Dumb scenario name");
            outputData.ConfigurationFilePath.Returns("\\Not\\A\\Valid\\Path");
            string expectedErrorMessage = $"Error while running basin: {outputData.BasinName}, scenario: {outputData.ScenarioName}, config file: {outputData.ConfigurationFilePath}";
            var damageAssessmentHandler = Substitute.ForPartsOf<DamageAssessmentHandler>();
            IExeWrapper exeWrapper = Substitute.For<IExeWrapper>();
            IWriter dataWriter = Substitute.For<IWriter>();
            IFloodDamageDomain dataDomain = Substitute.For<IFloodDamageDomain>();

            damageAssessmentHandler.ExeWrapper.Returns(exeWrapper);
            damageAssessmentHandler.DataWriter.Returns(dataWriter);
            damageAssessmentHandler.DataDomain.Returns(dataDomain);
            dataWriter
                .Configure()
                .WriteData(Arg.Any<IFloodDamageDomain>())
                .Returns(new []{outputData});
            exeWrapper
                .Configure()
                .ValidateRun(Arg.Any<IOutputData>())
                .Returns(false);
            
            // Define test actions.
            TestDelegate testAction = () => damageAssessmentHandler.Run();

            // Verify final expectations
            Assert.That(testAction, Throws.Exception.With.Message.EqualTo(expectedErrorMessage));
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
            IOutputData[] outputDataCollection = testFilePaths
                .Select(fp =>
                {
                    var outputData = Substitute.For<IOutputData>();
                    outputData.ConfigurationFilePath.Returns(fp);
                    return outputData;
                }).ToArray();

            damageAssessmentHandler.ExeWrapper.Returns(exeWrapper);
            damageAssessmentHandler.DataWriter.Returns(dataWriter);
            damageAssessmentHandler.DataDomain.Returns(dataDomain);
            dataWriter
                .Configure()
                .WriteData(Arg.Any<IFloodDamageDomain>())
                .Returns(outputDataCollection);
            exeWrapper
                .Configure()
                .ValidateRun(Arg.Any<IOutputData>())
                .Returns(true);

            // Define test actions.
            TestDelegate testAction = () => damageAssessmentHandler.Run();

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            foreach (IOutputData outputData in outputDataCollection)
            {
                // NSubstitute assert call was received with given argument.
                exeWrapper.Received().Run(outputData);
            }
        }
    }
}