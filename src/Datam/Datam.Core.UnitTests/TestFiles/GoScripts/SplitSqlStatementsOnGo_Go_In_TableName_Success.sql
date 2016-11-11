CREATE SCHEMA [test]
GO

CREATE TABLE [test].[Test]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[GO] VARCHAR(255) NOT NULL
)
GO


INSERT INTO [test].[GOTest] VALUES 
(1, 'Value 1'),
(2, 'Value 2')