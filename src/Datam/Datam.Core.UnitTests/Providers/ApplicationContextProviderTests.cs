using Datam.Core.Model;
using Datam.Core.Providers;
using Datam.Core.Services;
using Moq;
using NUnit.Framework;

namespace Datam.Core.UnitTests.Providers
{
    [TestFixture]
    public class ApplicationContextProviderTests
    {
        [Test]
        public void GetApplicationContext_Success()
        {
            var expected = new ApplicationContext()
            {
                Server = "myServer",
                Database = "myDatabase",
                OperationType = OperationType.Info,
                Password = string.Empty,
                Username = string.Empty,
                Port = "8080"
            };

            //Arrange
            var options = new Options()
            {
                Server = "myServer",
                Database = "myDatabase",
                OperationType = OperationType.Info,
                Password = string.Empty,
                Username = string.Empty,
                Port = "8080"
            };
            var configService = new Mock<IConfigService>();
            configService.Setup(c => c.GetPatchesFolder()).Returns("myFolder");
            configService.Setup(c => c.GetPatchesRegex()).Returns("myRegex");
            var optionsParserService = new Mock<IOptionsParserService>();
            optionsParserService.Setup(o => o.ParseOptions(It.IsAny<string[]>())).Returns(options);
            optionsParserService.Setup(o => o.GetOptions()).Returns(options);
            var appContextProvider = new ApplicationContextProvider(configService.Object, optionsParserService.Object);

            //Act
            var result = appContextProvider.GetApplicationContext();

            //Assert
            Assert.AreEqual(expected.Server, result.Server);
            Assert.AreEqual(expected.Database, result.Database);
            Assert.AreEqual(expected.OperationType, result.OperationType);
            Assert.AreEqual(expected.Password, result.Password);
            Assert.AreEqual(expected.Username, result.Username);
            Assert.AreEqual(expected.Port, result.Port);
            Assert.AreEqual("myFolder", result.PatchesFolder);
            Assert.AreEqual("myRegex", result.PatchesRegex);
        }
    }
}
