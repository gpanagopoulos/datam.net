-- EVERY DDL STATEMENT SHOULD BE FOLLOWED BY GO. EVERY DML Statement should end with ';'
CREATE TABLE [locations].[Country]
(
	[CountryId] INT NOT NULL,
	[Name] VARCHAR(255) NOT NULL
	CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId])
)
GO

INSERT INTO [locations].[Country] ([CountryId], [Name]) VALUES
(1, 'United Kingdom'),
(2, 'USA'),
(3, 'Italy'),
(4, 'Spain'),
(5, 'France'),
(6, 'Russia'),
(7, 'Ukraine'),
(8, 'Germany'),
(9, 'Greece'),
(10, 'Portugal'),
(11, 'Switzerland'),
(12, 'Poland'),
(13, 'Romania'),
(14, 'Bulgaria'),
(15, 'Denmark'),
(16, 'Norway');

ALTER TABLE [locations].[City]
ADD [CountryId] INT NULL
GO

ALTER TABLE [locations].[City]
ADD CONSTRAINT [FK_City_Country] FOREIGN KEY ([CountryId]) REFERENCES [locations].[Country]([CountryId])
GO

UPDATE city 
SET [CountryId] = (SELECT [CountryId] FROM [locations].[Country] WHERE [Name] = city.[Country]) 
FROM [locations].[City] city

ALTER TABLE [locations].[City]
ALTER COLUMN [CountryId] INT NOT NULL
GO

ALTER TABLE [locations].[City]
DROP COLUMN [Country]
GO
