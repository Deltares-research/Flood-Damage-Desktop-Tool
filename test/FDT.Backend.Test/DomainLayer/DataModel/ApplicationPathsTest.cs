﻿using System.IO;
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
        public void ChangeRootDirectoryWithoutExposureDirThrowsDirectoryNotFoundException()
        {
            var exposurePath = Path.Combine("something", "database", "Exposure");
            string expectedErrorMssg = $"Exposure directory does not exist at {exposurePath}";
            TestDelegate testAction = () => new ApplicationPaths().ChangeRootDirectory("something");

            Assert.That(
                testAction, 
                Throws.Exception.TypeOf<DirectoryNotFoundException>().With.Message.Contains(expectedErrorMssg));
        }

        [Test]
        public void ChangeRootDirectoryWithoutExposureSubdirectoriesThrowsException()
        {
            // 1. Define test data.
            string currentDir = Directory.GetCurrentDirectory();
            string rootDir = Path.Combine(currentDir, "testRootDir");
            string databasePath = Path.Combine(rootDir, "database");
            string exposurePath = Path.Combine(databasePath, "Exposure");
            string exceptionMessage = $"No basin subdirectories found at Exposure directory {exposurePath}";

            if (Directory.Exists(exposurePath))
                Directory.Delete(exposurePath, true);
            Directory.CreateDirectory(exposurePath);

            // 2. Define test action.
            TestDelegate testAction = () => new ApplicationPaths().ChangeRootDirectory(rootDir);

            // 3. Verify final expectations.
            Assert.That(testAction, Throws.Exception.With.Message.Contains(exceptionMessage));
        }

    }
}