using System;
using System.Threading;
using FIAT.Gui.CustomFields;
using FIAT.Gui.ViewModels;
using FIAT.TestUtils;
using NUnit.Framework;

namespace FIAT.Gui.Test.CustomFields
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