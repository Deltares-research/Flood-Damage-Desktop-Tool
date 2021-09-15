using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FDT.Backend.DomainLayer.IDataModel;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Backend.Test.PersistenceLayer
{

    public class PersistenceLayerTestData
    {
        private static IBasin GetTestBasin(string basinName, string projection, IEnumerable<IScenario> scenarios = null)
        {
            IBasin testBasin = Substitute.For<IBasin>();
            testBasin.BasinName.Returns(basinName);
            testBasin.Projection.Returns(projection);
            testBasin.Scenarios.Returns(scenarios);
            return testBasin;
        }

        private static IBasin GetTestBasinWithTestScenario(string scenarioName)
        {
            IBasin testBasin = GetTestBasin("ValidBasinName", "ValidProjection");
            IScenario testScenario = Substitute.For<IScenario>();
            testScenario.ScenarioName.Returns(scenarioName);
            testBasin.Scenarios.Returns(new[] { testScenario });
            return testBasin;
        }

        public static IEnumerable InvalidIBasin
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
                yield return new TestCaseData(GetTestBasin(null, null), typeof(ArgumentNullException), nameof(IBasin.BasinName));
                yield return new TestCaseData(GetTestBasin(string.Empty, null), typeof(ArgumentNullException), nameof(IBasin.BasinName));
                yield return new TestCaseData(GetTestBasin(validBasinName, null), typeof(ArgumentNullException), nameof(IBasin.Projection));
                yield return new TestCaseData(GetTestBasin(validBasinName, string.Empty), typeof(ArgumentNullException), nameof(IBasin.Projection));
                yield return new TestCaseData(GetTestBasin(validBasinName, validProjection), typeof(Exception), noValidScenariosMssg);
                yield return new TestCaseData(GetTestBasin(validBasinName, validProjection, Enumerable.Empty<IScenario>()), typeof(Exception), noValidScenariosMssg);
                yield return new TestCaseData(GetTestBasinWithTestScenario(null), typeof(Exception), invalidScenariosFoundMssg);
                yield return new TestCaseData(GetTestBasinWithTestScenario(string.Empty), typeof(Exception), invalidScenariosFoundMssg);
                yield return new TestCaseData(GetTestBasinWithFloodMaps(0), typeof(Exception), invalidFloodMapReturnPeriod);
            }
        }

        private static IBasin GetTestBasinWithFloodMaps(int returnPeriod)
        {
            IBasin testBasin = GetTestBasinWithTestScenario("DummyScenario");
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