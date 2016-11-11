-- EVERY T-SQL STATEMENT SHOULD BE FOLLOWED BY GO AND RETURN
CREATE SCHEMA [locations]
GO

CREATE TABLE [locations].[City]
(
	[CityId] INT NOT NULL,
	[Name] VARCHAR(255) NOT NULL,
	[Country] VARCHAR(255) NOT NULL,
	CONSTRAINT [PK_City] PRIMARY KEY ([CityId])
)
GO

INSERT INTO [locations].[City] ([CityId], [Name], [Country]) VALUES 
(1, N'London', 'United Kingdom'),
(2, N'Zurich', 'Switzerland'),
(3, N'Munich', 'Germany'),
(4, N'Paris', 'France'),
(5, N'Rome', 'Italy'),
(6, N'Barcelona', 'Spain'),
(7, N'Mardin', 'Spain'),
(8, N'Athens', 'Greece'),
(9, N'Milan', 'Italy'),
(10, N'New York', 'USA'),
(11, N'Birmingham', 'United Kingdom'),
(12, N'Birmingham', 'USA'),
(13, N'Moscow', 'Russia'),
(14, N'Lisbon', 'Portugal'),
(15, N'Kiev', 'Ukraine'),
(16, N'Sofia', 'Bulgaria'),
(17, N'Bucharest', 'Romania'),
(18, N'Warsaw', 'Poland'),
(19, N'Oslo', 'Norway'),
(20, N'Copenhagen', 'Denmark');