using NUnit.Framework;
using FDT.Gui.CustomFields;
using System;
using System.Threading;
using FDT.TestUtils;

namespace FDT.Gui.Test.CustomFields
{
    [TestFixture, Apartment(ApartmentState.STA)]
    class InputFieldFloodMapTest
    {
        [Test]
        [STAThread]
        public void TestInputFieldAsGui()
        {
            InputFieldFloodMap inputField = new InputFieldFloodMap();
            const string eventFloodMapStr = "Event Flood Map";
            const int eventReturnPeriod = 24;
            const string riskFloodMapStr = "Risk Flood Map";
            const int riskReturnPeriod = 42;
            var eventFloodMap = new FloodMap()
            {
                MapPath = eventFloodMapStr,
                ReturnPeriod = eventReturnPeriod,
            };
            var riskFloodMap = new FloodMapWithReturnPeriod()
            {
                MapPath = riskFloodMapStr,
                ReturnPeriod = riskReturnPeriod
            };
            var listMaps = new[] {eventFloodMap, riskFloodMap};
            inputField.FloodMap = listMaps[0];
            WpfTestHelper testHelper = new WpfTestHelper(inputField, "Test", () =>
            {
                inputField.FloodMap = inputField.FloodMap == eventFloodMap ? riskFloodMap : eventFloodMap;
            });
            testHelper.ShowDialog();
        }
    }
}
