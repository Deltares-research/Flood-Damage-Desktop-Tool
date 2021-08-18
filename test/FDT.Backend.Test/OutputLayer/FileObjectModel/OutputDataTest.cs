using FDT.Backend.OutputLayer.FileObjectModel;
using FDT.Backend.OutputLayer.IFileObjectModel;
using NUnit.Framework;

namespace FDT.Backend.Test.OutputLayer.FileObjectModel
{
    public class OutputDataTest
    {
        [Test]
        public void ConstructorTest()
        {
            var outputDataTest = new OutputData();
            Assert.That(outputDataTest, Is.Not.Null);
            Assert.That(outputDataTest, Is.InstanceOf<IOutputData>());
        }
    }
}