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
            IExeWrapper dummyWrapper = Substitute.For<IExeWrapper>();

            // Define test action.
            TestDelegate testAction = () => testHandler = new DamageAssessmentHandler(dummyDomain, dummyWrapper);
            
            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(testHandler, Is.Not.Null);
            Assert.That(testHandler, Is.InstanceOf<IRunnerHandler>());
            Assert.That(testHandler.DataDomain, Is.EqualTo(dummyDomain));
            Assert.That(testHandler.ExeWrapper, Is.EqualTo(dummyWrapper));
        }

        [Test]
        public void ConstructorThrowsExceptionWhenNullFloodDomain()
        {
            TestDelegate testAction = () => new DamageAssessmentHandler(null, Substitute.For<IExeWrapper>());
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("floodDomain"));
        }

        [Test]
        public void ConstructorThrowsExceptionWhenNullExeWrapper()
        {
            TestDelegate testAction = () => new DamageAssessmentHandler( Substitute.For<IFloodDamageDomain>(), null);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("exeWrapper"));
        }
    }
}