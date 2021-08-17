using System;
using FDT.Backend.ExeHandler;
using FDT.Backend.IDataModel;
using FDT.Backend.IExeHandler;
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
            TestDelegate testAction = () => testHandler = new DamageAssessmentHandler(dummyDomain);
            
            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(testHandler, Is.Not.Null);
            Assert.That(testHandler, Is.InstanceOf<IRunnerHandler>());
            Assert.That(testHandler.DataDomain, Is.EqualTo(dummyDomain));
            Assert.That(testHandler.ExeWrapper, Is.Not.Null);
            Assert.That(testHandler.ExeWrapper, Is.InstanceOf<IExeWrapper>());
        }

        [Test]
        public void ConstructorThrowsExceptionWhenNullFloodDomain()
        {
            TestDelegate testAction = () => new DamageAssessmentHandler(null);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("floodDomain"));
        }
    }
}