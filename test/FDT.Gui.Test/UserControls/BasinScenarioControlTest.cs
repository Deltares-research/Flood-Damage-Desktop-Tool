using System;
using System.Collections.ObjectModel;
using System.Threading;
using FDT.Gui.UserControls;
using FDT.Gui.ViewModels;
using FDT.TestUtils;
using NSubstitute;
using NUnit.Framework;

namespace FDT.Gui.Test.UserControls
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class BasinScenarioControlTest
    {
        [Test]
        [STAThread]
        public void TestWhenAddFloodMapUpdatesList()
        {
            var basin = Substitute.For<IBasinScenario>();
            var scenario = Substitute.For<IScenario>();
            basin.Scenarios = new ObservableCollection<IScenario>();
            scenario.FloodMaps = new ObservableCollection<IFloodMap>();
            var scenarioControl = new ScenarioControl();
            scenarioControl.Scenario = scenario;
            scenario.When(s => s.AddExtraFloodMap()).Do((t) => {
                var floodMap = Substitute.For<IFloodMap>();
                floodMap.HasReturnPeriod.Returns(true);
                scenario.FloodMaps.Add(floodMap);
            });

            WpfTestHelper testHelper = new WpfTestHelper(scenarioControl, "Adding Scenarios", null);
            testHelper.ShowDialog();
        }
    }
}