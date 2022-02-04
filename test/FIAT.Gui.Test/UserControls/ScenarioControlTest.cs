using System;
using System.Collections.ObjectModel;
using System.Threading;
using FIAT.Gui.UserControls;
using FIAT.TestUtils;
using NSubstitute;
using NUnit.Framework;
using IFloodMap = FIAT.Gui.ViewModels.IFloodMap;
using IScenario = FIAT.Gui.ViewModels.IScenario;

namespace FIAT.Gui.Test.UserControls
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class ScenarioControlTest
    {
        [Test, Explicit]
        [STAThread]
        public void TestWhenAddFloodMapUpdatesList()
        {
            var scenario = Substitute.For<IScenario>();
            scenario.FloodMaps = new ObservableCollection<IFloodMap>();
            var scenarioControl = new ScenarioEventControl();
            scenarioControl.Scenario = scenario;
            scenario.When( s => s.AddExtraFloodMap()).Do((t) => {
                var floodMap = Substitute.For<IFloodMap>();
                floodMap.HasReturnPeriod.Returns(true);
                scenario.FloodMaps.Add(floodMap);
            });

            WpfTestHelper testHelper = new WpfTestHelper(scenarioControl, "Adding Flood map fields", null);
            testHelper.ShowDialog();
        }

        [Test]
        [STAThread, Explicit]
        public void TestWhenRemoveFloodMapUpdatesList()
        {
            var scenarioControl = new ScenarioEventControl();
            var scenario = Substitute.For<IScenario>();
            scenarioControl.Scenario = scenario;
            scenario.FloodMaps = new ObservableCollection<IFloodMap>();
            foreach (string floodMapPath in new [] { "path0", "path1", "path2" })
            {
                var floodMap = Substitute.For<IFloodMap>();
                floodMap.MapPath.Returns(floodMapPath);
                scenario.FloodMaps.Add(floodMap);
            }
            scenario.When(s => s.AddExtraFloodMap()).Do((t) => {
                var floodMap = Substitute.For<IFloodMap>();
                floodMap.HasReturnPeriod.Returns(true);
                scenario.FloodMaps.Add(floodMap);
            });
            WpfTestHelper testHelper = new WpfTestHelper(scenarioControl, "Removing Flood map fields", null);
            testHelper.ShowDialog();
        }
    }
}