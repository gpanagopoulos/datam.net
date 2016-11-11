using Autofac;
using Datam.Core.Commands;
using Datam.Core.Container;
using Datam.Core.DataAccess;
using Datam.Core.Model;
using Moq;
using NUnit.Framework;

namespace Datam.Core.UnitTests.Container
{
    [TestFixture]
    public class CoreModuleTests
    {
        [Test]
        public void GetMigrateCommand_Success()
        {
            //Arrange
            var databaseUpdater = new Mock<IDatabaseUpdater>();
            var builder = new ContainerBuilder();
            builder.RegisterInstance(databaseUpdater.Object).As<IDatabaseUpdater>();
            builder.RegisterModule(new CoreModule());
            var container = builder.Build();

            //Act
            var command = container.ResolveNamed<ICommand>(OperationType.Migrate.ToString());

            //Assert
            Assert.IsTrue(command is MigrateCommand);
        }

        [Test]
        public void GetInfoCommand_Success()
        {
            //Arrange
            var databaseUpdater = new Mock<IDatabaseUpdater>();
            var builder = new ContainerBuilder();
            builder.RegisterInstance(databaseUpdater.Object).As<IDatabaseUpdater>();
            builder.RegisterModule(new CoreModule());
            var container = builder.Build();

            //Act
            var command = container.ResolveNamed<ICommand>(OperationType.Info.ToString());

            //Assert
            Assert.IsTrue(command is InfoCommand);
        }
    }
}
