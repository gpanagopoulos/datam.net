using Datam.Core.Helpers;
using NUnit.Framework;

namespace Datam.Core.UnitTests.Helpers
{
    [TestFixture]
    public class PathHelperTests
    {
        [Test]
        public void GetRelativeLocation_Success()
        {
            string expected = @"D:\datam.net\src\Datam\Scripts\SqlServer";
            //Arrange
            string codebase = "file:///D://datam.net/src/Datam/Datam.Host/bin/Debug/Datam.Core.DLL";
            string relativePath = @"..\..\..\Scripts\SqlServer";

            //Act
            string result = PathHelper.GetRelativeLocation(codebase, relativePath);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}


