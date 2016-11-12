-- EVERY DDL STATEMENT SHOULD BE FOLLOWED BY GO. EVERY DML Statement should end with ';'
CREATE TABLE city
(
	city_id INT NOT NULL,
	name VARCHAR(255) NOT NULL,
	country VARCHAR(255) NOT NULL,
	CONSTRAINT pk_city PRIMARY KEY (city_id)
)
GO

INSERT INTO city (city_id, name, country) VALUES 
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