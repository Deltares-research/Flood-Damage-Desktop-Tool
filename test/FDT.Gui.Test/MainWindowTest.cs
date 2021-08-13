using System;
using System.Linq;
using System.Threading;
using FDT.Gui.ViewModels;
using NUnit.Framework;

namespace FDT.Gui.Test
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class MainWindowTest
    {
        [Test]
        [STAThread]
        public void TestRunMainWindowWithDummyData()
        {
            var mainWindowControl = new MainWindow();
            var viewModel = mainWindowControl.DataContext as MainWindowViewModel;
            Assert.That(viewModel, Is.Not.Null);

            viewModel.GetBasinsDirectories = () =>
            {
                return new[]
                {
                    "C-1", "C-2", "C-3", "C-4"
                };
            };

            // Define some stuff for EVENT based scenario
            EventBasedScenario eventBasinScenario = viewModel.BasinScenarios.OfType<EventBasedScenario>().Single();
            eventBasinScenario.IsEnabled = true;
            IScenario defaultEventScenario = eventBasinScenario.Scenarios.Single();
            defaultEventScenario.ScenarioName = "First EVENT created scenario";
            defaultEventScenario.FloodMaps[0].MapPath = "Some//Simple//Path";
            defaultEventScenario.FloodMaps.Add(new FloodMap() {MapPath = "Some//Way//Longer//Path//To//Test"});

            // Define some stuff for Risk based scenario
            RiskBasedScenario riskBasinScenario = viewModel.BasinScenarios.OfType<RiskBasedScenario>().Single();
            riskBasinScenario.IsEnabled = true;
            IScenario defaultRiskScenario = riskBasinScenario.Scenarios.Single();
            defaultRiskScenario.ScenarioName = "First RISK created scenario";
            defaultRiskScenario.FloodMaps[0].MapPath = "Some//Simple//Path";
            defaultRiskScenario.FloodMaps[0].ReturnPeriod = 42;
            defaultRiskScenario.FloodMaps.Add(new FloodMap()
            {
                MapPath = "Some//Way//Longer//Path//To//Test",
                ReturnPeriod = 24
            });

            mainWindowControl.ShowDialog();
        }
    }
}