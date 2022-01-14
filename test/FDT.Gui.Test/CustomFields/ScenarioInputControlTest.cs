using System;
using System.Threading;
using FDT.Gui.CustomFields;
using FDT.Gui.ViewModels;
using FDT.TestUtils;
using NUnit.Framework;

namespace FDT.Gui.Test.CustomFields
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class ScenarioInputControlTest
    {
        [Test, Explicit]
        [STAThread]
        public void TestGivenScenarioInputControl()
        {
            ScenarioInputControl inputField = new ScenarioInputControl();
            var scenario = new Scenario<FloodMapWithReturnPeriod>();
            inputField.Scenario = scenario;

            WpfTestHelper testHelper = new WpfTestHelper(inputField, "Setting Scenario Values", () => {});
            testHelper.ShowDialog();
        }
    }
}