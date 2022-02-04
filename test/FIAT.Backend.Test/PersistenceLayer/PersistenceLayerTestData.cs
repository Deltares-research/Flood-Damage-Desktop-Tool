using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FIAT.Backend.DomainLayer.IDataModel;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Backend.Test.PersistenceLayer
{

    public class PersistenceLayerTestData
    {
        private static IFloodDamageBasin GetTestFloodDamageBasin(string basinName, string projection, IEnumerable<IScenario> scenarios = null)
        {
            IFloodDamageBasin testBasin = Substitute.For<IFloodDamageBasin>();
            testBasin.BasinName.Returns(basinName);
            testBasin.Projection.Returns(projection);
            testBasin.Scenarios.Returns(scenarios);
            return testBasin;
        }

        private static IFloodDamageBasin GetTestFloodDamageBasinWithTestScenario(string scenarioName)
        {
            IFloodDamageBasin testBasin = GetTestFloodDamageBasin("ValidBasinName", "ValidProjection");
            IScenario testScenario = Substitute.For<IScenario>();
            testScenario.ScenarioName.Returns(scenarioName);
            testBasin.Scenarios.Returns(new[] { testScenario });
            return testBasin;
        }

        public static IEnumerable InvalidIFloodDamageBasin
        {
            get
            {
                const string validBasinName = "ValidBasinName";
                const string validProjection = "Projection";
                const string noValidScenariosMssg = "No valid scenarios were provided.";
                const string invalidScenariosFoundMssg = "All selected scenarios should contain a valid name.";
                const string invalidFloodMapReturnPeriod =
                    "All selected Flood Maps with return period should be greater than 0.";
                yield return new TestCaseData(null, typeof(ArgumentNullException), "basinData");
                yield return new TestCaseData(GetTestFloodDamageBasin(null, null), typeof(ArgumentNullException), nameof(IBasin.BasinName));
                yield return new TestCaseData(GetTestFloodDamageBasin(string.Empty, null), typeof(ArgumentNullException), nameof(IBasin.BasinName));
                yield return new TestCaseData(GetTestFloodDamageBasin(validBasinName, null), typeof(ArgumentNullException), nameof(IBasin.Projection));
                yield return new TestCaseData(GetTestFloodDamageBasin(validBasinName, string.Empty), typeof(ArgumentNullException), nameof(IBasin.Projection));
                yield return new TestCaseData(GetTestFloodDamageBasin(validBasinName, validProjection), typeof(Exception), noValidScenariosMssg);
                yield return new TestCaseData(GetTestFloodDamageBasin(validBasinName, validProjection, Enumerable.Empty<IScenario>()), typeof(Exception), noValidScenariosMssg);
                yield return new TestCaseData(GetTestFloodDamageBasinWithTestScenario(null), typeof(Exception), invalidScenariosFoundMssg);
                yield return new TestCaseData(GetTestFloodDamageBasinWithTestScenario(string.Empty), typeof(Exception), invalidScenariosFoundMssg);
                yield return new TestCaseData(GetTestFloodDamageBasinWithFloodMaps(0), typeof(Exception), invalidFloodMapReturnPeriod);
            }
        }

        private static IFloodDamageBasin GetTestFloodDamageBasinWithFloodMaps(int returnPeriod)
        {
            IFloodDamageBasin testBasin = GetTestFloodDamageBasinWithTestScenario("DummyScenario");
            IFloodMapWithReturnPeriod[] floodMaps = null;
            if (returnPeriod >= 0)
            {
                var floodMap = Substitute.For<IFloodMapWithReturnPeriod>();
                floodMap.ReturnPeriod.Returns(returnPeriod);
                floodMaps = new[] { floodMap };
            }

            testBasin.Scenarios.Single().FloodMaps.Returns(floodMaps);
            return testBasin;
        }
    }
}