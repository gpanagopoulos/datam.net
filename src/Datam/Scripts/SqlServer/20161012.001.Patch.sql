-- EVERY DDL STATEMENT SHOULD BE FOLLOWED BY GO. EVERY DML Statement should end with ';'
CREATE SCHEMA [test]
GO

CREATE TABLE [test].[Test]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] VARCHAR(255) NOT NULL
)
GO


INSERT INTO [test].[Test] VALUES 
(1, 'Value 1'),
(2, 'Value 2');