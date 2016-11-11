using Datam.Core.ScriptReader;
using Datam.Core.Services;
using Moq;
using NUnit.Framework;

namespace Datam.Core.UnitTests.ScriptReader
{
    [TestFixture]
    public class FileScriptReaderTests
    {
        [Test]
        public void GetScriptName_Success()
        {
            string expected = "20160101.0001.Patch.sql";
            
            //Arrange
            string dummyPath = @".\Datam\Scripts\SqlServer\20160101.0001.Patch.sql";
            var configService = new Mock<IConfigService>();
            var scriptReader = new FileScriptReader(configService.Object);

            //Act
            string result = scriptReader.GetScriptName(dummyPath);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetScriptName_WrongPath_Success()
        {
            string expected = string.Empty;

            //Arrange
            string dummyPath = @"C:\temp\Scripts\";
            var configService = new Mock<IConfigService>();
            var scriptReader = new FileScriptReader(configService.Object);

            //Act
            string result = scriptReader.GetScriptName(dummyPath);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetScriptName_DifferentFileName()
        {
            string expected = "20160101.0002.Patch.sql";

            //Arrange
            string dummyPath = @"C:\temp\Scripts\20160101.0001.Patch.sql";
            var configService = new Mock<IConfigService>();
            var scriptReader = new FileScriptReader(configService.Object);

            //Act
            string result = scriptReader.GetScriptName(dummyPath);

            //Assert
            Assert.AreNotEqual(expected, result);
        }

        [Test]
        public void GetScripts_Success()
        {
            //Arrange
            string sqlServerScriptsPath = @"..\..\..\Scripts\SqlServer";
            string patchesRegex = "*.Patch.sql";
            var configService = new Mock<IConfigService>();
            configService.Setup(c => c.GetPatchesFolder()).Returns(sqlServerScriptsPath);
            configService.Setup(c => c.GetPatchesRegex()).Returns(patchesRegex);
            var scriptReader = new FileScriptReader(configService.Object);

            //Act
            var results = scriptReader.GetScripts();

            //Assert
            Assert.IsNotEmpty(results);
        }

    }
}
