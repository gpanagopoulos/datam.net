using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Datam.Core.Helpers;

namespace Datam.Core.UnitTests.Helpers
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void SplitSqlStatementsOnGo_Success()
        {
            //Arrange
            string relativePath = @".\TestFiles\GoScripts\SplitSqlStatementsOnGo_Success.sql";
            string codebase = Assembly.GetExecutingAssembly().CodeBase;
            string scriptPath = PathHelper.GetRelativeLocation(codebase, relativePath);
            string sqlScript = File.ReadAllText(scriptPath);

            //Act
            IEnumerable<string> scripts = sqlScript.SplitSqlStatementsOnGo();

            //Assert
            Assert.AreEqual(scripts.Count(), 3);
            Assert.IsFalse(scripts.Any(s => s.EndsWith("GO")));
        }

        [Test]
        public void SplitSqlStatementsOnGo_Go_In_Value_Success()
        {
            //Arrange
            string relativePath = @".\TestFiles\GoScripts\SplitSqlStatementsOnGo_Go_In_Value_Success.sql";
            string codebase = Assembly.GetExecutingAssembly().CodeBase;
            string scriptPath = PathHelper.GetRelativeLocation(codebase, relativePath);
            string sqlScript = File.ReadAllText(scriptPath);

            //Act
            IEnumerable<string> scripts = sqlScript.SplitSqlStatementsOnGo();

            //Assert
            Assert.AreEqual(scripts.Count(), 3);
            Assert.IsFalse(scripts.Any(s => s.EndsWith("GO")));
        }

        [Test]
        public void SplitSqlStatementsOnGo_Go_In_TableName_Success()
        {
            //Arrange
            string relativePath = @".\TestFiles\GoScripts\SplitSqlStatementsOnGo_Go_In_TableName_Success.sql";
            string codebase = Assembly.GetExecutingAssembly().CodeBase;
            string scriptPath = PathHelper.GetRelativeLocation(codebase, relativePath);
            string sqlScript = File.ReadAllText(scriptPath);

            //Act
            IEnumerable<string> scripts = sqlScript.SplitSqlStatementsOnGo();

            //Assert
            Assert.AreEqual(scripts.Count(), 3);
            Assert.IsFalse(scripts.Any(s => s.EndsWith("GO")));
        }

        [Test]
        public void SplitSqlStatementsOnGo_EndsWithGo_Success()
        {
            //Arrange
            string relativePath = @".\TestFiles\GoScripts\SplitSqlStatementsOnGo_EndsWithGo_Success.sql";
            string codebase = Assembly.GetExecutingAssembly().CodeBase;
            string scriptPath = PathHelper.GetRelativeLocation(codebase, relativePath);
            string sqlScript = File.ReadAllText(scriptPath);

            //Act
            IEnumerable<string> scripts = sqlScript.SplitSqlStatementsOnGo();

            //Assert
            Assert.AreEqual(scripts.Count(), 2);
            Assert.IsFalse(scripts.Any(s => s.EndsWith("GO")));
        }

    }
}
