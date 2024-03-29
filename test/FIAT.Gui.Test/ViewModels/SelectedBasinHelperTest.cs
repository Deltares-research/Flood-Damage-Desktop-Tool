﻿using System;
using FIAT.Backend.DomainLayer.IDataModel;
using FIAT.Gui.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace FIAT.Gui.Test.ViewModels
{
    public class SelectedBasinHelperTest
    {
        static object[] CrsCodeCases =
        {
            new object[] { "EPSG:4242", "EPSG:4242" },
            new object[] { "epsg:2424", "epsg:2424" },
            new object[] { "ESRI:4242", "ESRI:4242" },
            new object[] { "esri:2424", "esri:2424" },
            new object[] { "PROJCS[\"NAD83(HARN) / Florida East(ftUS)\",GEOGCS[\"NAD83(HARN)\",DATUM[\"D_North_American_1983_HARN\",SPHEROID[\"GRS_1980\",6378137,298.257222101]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"latitude_of_origin\",24.33333333333333],PARAMETER[\"central_meridian\",-81],PARAMETER[\"scale_factor\",0.999941177],PARAMETER[\"false_easting\",656166.667],PARAMETER[\"false_northing\",0],UNIT[\"Foot_US\",0.30480060960121924]]", "NAD83(HARN) / Florida East(ftUS)"},
            new object[] { "projcs[\"NAD83(HARN) / Florida East(ftUS)\",GEOGCS[\"NAD83(HARN)\",DATUM[\"D_North_American_1983_HARN\",SPHEROID[\"GRS_1980\",6378137,298.257222101]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"latitude_of_origin\",24.33333333333333],PARAMETER[\"central_meridian\",-81],PARAMETER[\"scale_factor\",0.999941177],PARAMETER[\"false_easting\",656166.667],PARAMETER[\"false_northing\",0],UNIT[\"Foot_US\",0.30480060960121924]]", "NAD83(HARN) / Florida East(ftUS)"},
            new object[] { "GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137,298.257223563]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]]", "GCS WGS 1984" },
            new object[] { "geogcs[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137,298.257223563]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]]", "GCS WGS 1984" }
        };

        [Test]
        public void ConstructorTest()
        {
            // Define test data
            SelectBasinHelper selectBasinHelper = null;

            // Define test delegate
            TestDelegate testAction = () => selectBasinHelper = new SelectBasinHelper();

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(selectBasinHelper, Is.Not.Null);
        }

        [Test]
        public void ChangeBasinThrowsExceptionWhenBasinIsNull()
        {
            TestDelegate testAction = () => new SelectBasinHelper().GetSelectedBasinWarning(null);
            Assert.That(testAction, Throws.Exception.TypeOf<ArgumentNullException>().With.Message.Contains("selectedBasin"));
        }

        [Test]
        public void ChangeBasinDoesNothingWhenBasinAlreadySelected()
        {
            // Define test data.
            IBasin testBasin = Substitute.For<IBasin>();
            testBasin.Projection.Returns("aProjection");
            int showWarnings = 0;
            var selectBasinHelper = new SelectBasinHelper();
            string warningMessage = string.Empty;
            
            // Define test action.
            TestDelegate testAction = () =>
            {
                warningMessage = selectBasinHelper.GetSelectedBasinWarning(testBasin);
                warningMessage = selectBasinHelper.GetSelectedBasinWarning(testBasin);
            };

            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(warningMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ChangeBasinShowsWarningMessageWhenBasinHasNoProjection(string projectionName)
        {
            // Define test data
            IBasin testBasin = Substitute.For<IBasin>();
            const string basinName = "aName";
            testBasin.BasinName.Returns(basinName);
            testBasin.Projection.Returns(projectionName);
            string warningMessage = string.Empty;
            string expectedMessage = $"The area of interest {basinName} does not have an associated projection file.";

            // Define test action
            TestDelegate testAction = () => warningMessage = new SelectBasinHelper().GetSelectedBasinWarning(testBasin);
            
            // Verify final expectations.
            Assert.That(testAction, Throws.Nothing);
            Assert.That(warningMessage, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCaseSource(nameof(CrsCodeCases))]
        public void ChangeBasinShowWarningMessageReturnsExpectedString(string wkidString, string expectedCrsCode)
        {
            // Define test data
            IBasin testBasin = Substitute.For<IBasin>();
            testBasin.Projection.Returns(wkidString);
            var basinHelper = new SelectBasinHelper();
            string resultWarningMessage = string.Empty;
            string expectedWarningMessage =
                $"Only use flood maps with coordinate system {expectedCrsCode} for this area of interest.";

            // Define test delegate
            TestDelegate testAction = () => resultWarningMessage = basinHelper.GetSelectedBasinWarning(testBasin);
            
            // Verify final expectations
            Assert.That(testAction, Throws.Nothing);
            Assert.That(resultWarningMessage, Is.EqualTo(expectedWarningMessage));
        }
    }
}