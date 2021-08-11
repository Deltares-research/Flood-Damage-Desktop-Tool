using NUnit.Framework;
using FDT.Gui.CustomFields;
using System;
using System.Threading;
using System.Windows;
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
            WpfTestHelper testHelper = new WpfTestHelper(inputField, "Test", () => { });
            testHelper.ShowDialog();
        }
    }
}
