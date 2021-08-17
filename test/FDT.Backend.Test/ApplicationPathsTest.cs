using System;
using System.IO;
using NUnit.Framework;

namespace FDT.Backend.Test
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
        [TestCase(null, typeof(ArgumentNullException))]
        [TestCase("", typeof(ArgumentNullException))]
        [TestCase("test//Path", typeof(DirectoryNotFoundException))]
        public void UpdateExposurePathTestWithInvalidArgumentsThrowsException(string exposurePath, Type exceptionType)
        {
            var applicationPaths = new ApplicationPaths();
            TestDelegate testAction = () => applicationPaths.UpdateExposurePath(exposurePath);
            Assert.That(testAction, Throws.TypeOf(exceptionType));
        }
    }
}