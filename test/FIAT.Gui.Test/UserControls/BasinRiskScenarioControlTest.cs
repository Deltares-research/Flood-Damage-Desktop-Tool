﻿using System;
using System.Collections.ObjectModel;
using System.Threading;
using FIAT.Gui.UserControls;
using FIAT.Gui.ViewModels;
using FIAT.TestUtils;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Gui.Test.UserControls
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class BasinRiskScenarioControlTest
    {
        [Test, Explicit]
        [STAThread]
        public void TestWhenAddFloodMapUpdatesList()
        {
            var basin = Substitute.For<IBasinScenario>();
            var scenario = Substitute.For<IScenario>();
            var _scenarios = new ObservableCollection<IScenario>();
            basin.Scenarios.Returns(_scenarios);
            scenario.FloodMaps = new ObservableCollection<IFloodMap>();
            var basinScenarioControl = new BasinRiskScenarioControl();
            basinScenarioControl.BasinScenario = basin;
            scenario.When(s => s.AddExtraFloodMap()).Do((t) => {
                var floodMap = Substitute.For<IFloodMap>();
                floodMap.HasReturnPeriod.Returns(true);
                scenario.FloodMaps.Add(floodMap);
            });

            WpfTestHelper testHelper = new WpfTestHelper(basinScenarioControl, "Adding Scenarios", null);
            testHelper.ShowDialog();
        }

        [Test, Explicit]
        [STAThread]
        public void TestWithRealData()
        {
            var basin = new EventBasedScenario();
            var basinScenarioControl = new BasinRiskScenarioControl();
            basinScenarioControl.BasinScenario = basin;
            WpfTestHelper testHelper = new WpfTestHelper(basinScenarioControl, "Adding Scenarios", null);
            testHelper.ShowDialog();
        }
    }
}