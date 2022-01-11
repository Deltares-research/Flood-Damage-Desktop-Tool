using FDT.Backend.DomainLayer.DataModel;
using FDT.Backend.DomainLayer.IDataModel;
using NUnit.Framework;

namespace FDT.Backend.Test.DomainLayer.DataModel
{
    public class ApplicationPathsTest
    {
        [Test]
        public void ConstructorTest()
        {
            var applicationPaths = new ApplicationPaths();
            Assert.That(applicationPaths, Is.Not.Null);
            Assert.That(applicationPaths, Is.InstanceOf<IApplicationPaths>());
            Assert.That(applicationPaths.RootPath, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void UpdateSelectedBasinPathWithoutExposureDirThrowsNothing()
        {
            // TestDelegate testAction = () => new ApplicationPaths().UpdateSelectedBasin("something");
            // Assert.That(testAction, Throws.Nothing);
            Assert.Fail("needs rework");
        }

    }
}