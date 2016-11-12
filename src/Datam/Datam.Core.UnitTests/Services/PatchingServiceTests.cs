using System;
using System.Collections.Generic;
using Datam.Core.DataAccess;
using Datam.Core.Model;
using Datam.Core.ScriptReader;
using Datam.Core.Services;
using Moq;
using NUnit.Framework;

namespace Datam.Core.UnitTests.Services
{
    [TestFixture]
    public class PatchingServiceTests
    {
        [Test]
        public void Migrate_Init_Called_Success()
        {
            //Arrange
            var scriptReader = new Mock<IScriptReader>();
            var databaseUpdater = new Mock<IDatabaseUpdater>();
            databaseUpdater.Setup(d => d.HasInitialised()).Returns(false);

            var patchingService = new PatchingService(databaseUpdater.Object, scriptReader.Object);

            //Act
            patchingService.Migrate(s => { });

            //Assert
            databaseUpdater.Verify(d => d.Initialise(), Times.Once());
        }

        [Test]
        public void Migrate_AlreadyInit_Success()
        {
            string expected = "Applied: 20160102.002.Patch.sqlApplied: 20160103.001.Patch.sql";
            string result = string.Empty;
            //Arrange
            var scriptReader = new Mock<IScriptReader>();
            scriptReader.Setup(s => s.GetScripts()).Returns(new List<string>()
            {
                "20160101.001.Patch.sql", "20160101.002.Patch.sql", "20160101.003.Patch.sql", "20160101.004.Patch.sql",
                "20160101.005.Patch.sql", "20160102.001.Patch.sql", "20160102.002.Patch.sql", "20160103.001.Patch.sql"
            });
            scriptReader.Setup(s => s.GetScriptName(It.IsAny<string>())).Returns<string>(s => s);

            var databaseUpdater = new Mock<IDatabaseUpdater>();
            databaseUpdater.Setup(d => d.HasInitialised()).Returns(true);//already initialised
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160101.001.Patch.sql")).Returns(true);
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160101.002.Patch.sql")).Returns(true);
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160101.003.Patch.sql")).Returns(true);
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160101.004.Patch.sql")).Returns(true);
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160101.005.Patch.sql")).Returns(true);
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160102.001.Patch.sql")).Returns(true);
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160102.002.Patch.sql")).Returns(false);
            databaseUpdater.Setup(d => d.HasScriptExecuted("20160103.001.Patch.sql")).Returns(false);

            databaseUpdater.Setup(d => d.UpgradeVersion("20160102.002.Patch.sql")).Returns(true);
            databaseUpdater.Setup(d => d.UpgradeVersion("20160103.001.Patch.sql")).Returns(true);

            var patchingService = new PatchingService(databaseUpdater.Object, scriptReader.Object);
            
            //Act
            patchingService.Migrate(s => result += s);

            //Assert
            databaseUpdater.Verify(d => d.Initialise(), Times.Never());

            scriptReader.Verify(s => s.ReadAllScriptText("20160102.002.Patch.sql"), Times.Once);
            scriptReader.Verify(s => s.ReadAllScriptText("20160103.001.Patch.sql"), Times.Once);

            databaseUpdater.Verify(d => d.ExecuteScript(It.IsAny<string>()), Times.Exactly(2));

            databaseUpdater.Verify(d => d.UpgradeVersion("20160102.002.Patch.sql"), Times.Once);
            databaseUpdater.Verify(d => d.UpgradeVersion("20160103.001.Patch.sql"), Times.Once);

            databaseUpdater.Verify(d => d.Commit(), Times.Once);
            databaseUpdater.Verify(d => d.Close(), Times.Once);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Migrate_Error_RollsBack()
        {
            string result = string.Empty;
            //Arrange
            var scriptReader = new Mock<IScriptReader>();
            scriptReader.Setup(s => s.GetScripts()).Returns(new List<string>() { "20160101.001.Patch.sql" });
            var databaseUpdater = new Mock<IDatabaseUpdater>();
            var patchingService = new PatchingService(databaseUpdater.Object, scriptReader.Object);

            //Act
            patchingService.Migrate(s => result += s);

            //Assert
            databaseUpdater.Verify(d => d.Rollback(), Times.Once);
            databaseUpdater.Verify(d => d.Commit(), Times.Never);
            databaseUpdater.Verify(d => d.Close(), Times.Once);
        }

        [Test]
        public void Migrate_NoScripts_Success()
        {
            string result = string.Empty;
            //Arrange
            var scriptReader = new Mock<IScriptReader>();
            scriptReader.Setup(s => s.GetScripts()).Returns(new List<string>() {  });
            var databaseUpdater = new Mock<IDatabaseUpdater>();
            var patchingService = new PatchingService(databaseUpdater.Object, scriptReader.Object);

            //Act
            patchingService.Migrate(s => result += s);

            //Assert
            databaseUpdater.Verify(d => d.Commit(), Times.Once);
            databaseUpdater.Verify(d => d.Close(), Times.Once);
        }

        [Test]
        public void GetInfo_Success()
        {
            string expected = "Filename | DatetimeApplied20160101.0001.Patch.sql | 01/01/2016 00:00:0020160101.0002.Patch.sql | 01/01/2016 00:00:0020160101.0003.Patch.sql | 01/01/2016 00:00:0020160101.0004.Patch.sql | 01/01/2016 00:00:00";
            string result = string.Empty;

            //Arrange
            var dataTimeApplied = DateTime.Parse("2016-01-01 00:00:00");
            var databaseUpdater = new Mock<IDatabaseUpdater>();
            databaseUpdater.Setup(o => o.GetMigrationInfo()).Returns(new List<Migration>()
            {
                new Migration() { DateTimeApplied = dataTimeApplied, Filename = "20160101.0001.Patch.sql"},
                new Migration() { DateTimeApplied = dataTimeApplied, Filename = "20160101.0002.Patch.sql"},
                new Migration() { DateTimeApplied = dataTimeApplied, Filename = "20160101.0003.Patch.sql"},
                new Migration() { DateTimeApplied = dataTimeApplied, Filename = "20160101.0004.Patch.sql"}
            });
            var scriptReader = new Mock<IScriptReader>();
            var patchingService = new PatchingService(databaseUpdater.Object, scriptReader.Object);

            //Act
            patchingService.GetInfo(res => result += res);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetInfo_Empty_Success()
        {
            string expected = "Filename | DatetimeApplied";
            string result = string.Empty;

            //Arrange
            var databaseUpdater = new Mock<IDatabaseUpdater>();
            databaseUpdater.Setup(o => o.GetMigrationInfo()).Returns(new List<Migration>());
            var scriptReader = new Mock<IScriptReader>();
            var patchingService = new PatchingService(databaseUpdater.Object, scriptReader.Object);

            //Act
            patchingService.GetInfo(res => result += res);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
