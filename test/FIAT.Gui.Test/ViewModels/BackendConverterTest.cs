﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Gui.ViewModels;
using NSubstitute;
using NUnit.Framework;
using FloodMap = FIAT.Backend.DomainLayer.DataModel.FloodMap;
using FloodMapWithReturnPeriod = FIAT.Backend.DomainLayer.DataModel.FloodMapWithReturnPeriod;
using IFloodMap = FIAT.Gui.ViewModels.IFloodMap;
using IScenario = FIAT.Gui.ViewModels.IScenario;

namespace FIAT.Gui.Test.ViewModels
{
    public class BackendConverterTest
    {
        [Test]
        public void TestConvertBasinScenariosWithValidArguments()
        {
            // Define test data.
            IBasinScenario testBasinScenario = Substitute.For<IBasinScenario>();
            IBasinScenario testDisabledScenario = Substitute.For<IBasinScenario>();
            testBasinScenario.IsEnabled.Returns(true);
            testDisabledScenario.IsEnabled.Returns(false);

            IScenario testScenario = Substitute.For<IScenario>();
            testScenario.ScenarioName.Returns("aName");

            testBasinScenario.Scenarios.Returns(new ObservableCollection<IScenario>(new List<IScenario> { testScenario }));
            FIAT.Backend.DomainLayer.IDataModel.IScenario[] resultScenarios = null;
            var basinScenarios = new List<IBasinScenario> {testBasinScenario, testDisabledScenario};
            // Define test action.
            TestDelegate testAction = () => resultScenarios = BackendConverter.ConvertBasinScenarios(basinScenarios).ToArray();
            
            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(resultScenarios, Is.Not.Empty.Or.Null);
            Assert.That(resultScenarios.Count(), Is.EqualTo(1));
            var resultScenario = resultScenarios.Single();
            Assert.That(resultScenario.ScenarioName, Is.EqualTo(testScenario.ScenarioName));
        }

        [Test]
        public void TestConvertBasinScenariosThrowsExceptionWithNullArguments()
        {
            TestDelegate testAction = () => BackendConverter.ConvertBasinScenarios(null);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("basinScenarios"));
        }

        [Test]
        public void TestConvertScenariosThrowsExceptionWithNullArguments()
        {
            TestDelegate testAction = () => BackendConverter.ConvertScenarios(null).ToArray();
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("scenarios"));
        }

        [Test]
        public void TestConvertFloodMapsThrowsExceptionWithNullArguments()
        {
            TestDelegate testAction = () => BackendConverter.ConvertFloodMaps(null, default(FloodMapType)).ToArray();
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("floodMaps"));
        }

        [Test]
        [TestCase(false, typeof(FloodMap))]
        [TestCase(true, typeof(FloodMapWithReturnPeriod))]
        public void TestConvertFloodMaps(bool hasReturnPeriod, Type expectedType)
        {
            var testFloodMap = Substitute.For<IFloodMap>();
            testFloodMap.HasReturnPeriod.Returns(hasReturnPeriod);
            IEnumerable<IFloodMapBase> convertedFloodMaps = Enumerable.Empty<IFloodMapBase>();
            TestDelegate testAction = () =>
                convertedFloodMaps = BackendConverter.ConvertFloodMaps(new[] {testFloodMap}, default(FloodMapType));
            Assert.That(testAction, Throws.Nothing);
            Assert.That(convertedFloodMaps.Single(), Is.InstanceOf<IFloodMapBase>());
            Assert.That(convertedFloodMaps.Single(), Is.InstanceOf(expectedType));
        }
    }
}