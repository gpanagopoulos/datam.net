using System;
using System.Reflection.Emit;
using Datam.Core.Model;
using Datam.Core.Services;
using NUnit.Framework;


namespace Datam.Core.UnitTests.Services
{
    [TestFixture]
    public class OptionsParserServiceTests
    {
        [Test]
        public void ParseOptions_Migrate_Success()
        {
            //Arrange
            string argsS = "-o Migrate -d myDatabase -s myServer -u myUsername -p myPassword -t 8080";
            string[] args = argsS.Split(' ');
            var optionsService = new OptionsParserService();

            //Act
            var options = optionsService.ParseOptions(args);

            //Assert
            Assert.AreEqual(options.Database, "myDatabase");
            Assert.AreEqual(options.Server, "myServer");
            Assert.AreEqual(options.Username, "myUsername");
            Assert.AreEqual(options.Password, "myPassword");
            Assert.AreEqual(options.Port, "8080");
            Assert.AreEqual(options.OperationType, OperationType.Migrate);
        }

        [Test]
        public void ParseOptions_Server_Username_Password_Database_Optional_Success()
        {
            //Arrange
            string argsS = "-o Info";
            string[] args = argsS.Split(' ');
            var optionsService = new OptionsParserService();

            //Act
            var options = optionsService.ParseOptions(args);

            //Assert
            Assert.AreEqual(options.OperationType, OperationType.Info);
        }

        [Test]
        public void ParseOptions_OperationType_Required_Failure()
        {
            //Arrange
            string argsS = "-d myDatabase -s myServer -u myUsername -p myPassword -t 8080";

            string[] args = argsS.Split(' ');
            var optionsService = new OptionsParserService();

            //Act
            TestDelegate parseOptionsErrorAction = () => optionsService.ParseOptions(args);

            //Act & Assert
            Assert.Catch(typeof(ApplicationException), parseOptionsErrorAction);
        }


    }
}
