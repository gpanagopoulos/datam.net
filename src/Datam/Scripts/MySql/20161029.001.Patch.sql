-- EVERY DDL STATEMENT SHOULD BE FOLLOWED BY GO. EVERY DML Statement should end with ';'
CREATE TABLE country
(
	country_id INT NOT NULL PRIMARY KEY,
	name VARCHAR(255) NOT NULL
)
GO

INSERT INTO country (country_id, name) VALUES
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

ALTER TABLE city
ADD COLUMN country_id INT NULL
GO

ALTER TABLE city
ADD CONSTRAINT fk_city_country FOREIGN KEY (country_id) REFERENCES country(country_id)
GO

UPDATE city
INNER JOIN country ON city.country = country.name
SET city.country_id = country.country_id;

ALTER TABLE city
MODIFY COLUMN country_id INT NOT NULL
GO

ALTER TABLE city
DROP COLUMN country
GO
