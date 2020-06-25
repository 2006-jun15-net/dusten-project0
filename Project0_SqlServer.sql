CREATE SCHEMA Project0;
GO

CREATE TABLE Project0.Store (

	Id INT IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL
);

CREATE TABLE Project0.Customer (

	Id INT IDENTITY(1, 1) NOT NULL, 
	StoreId INT NULL,
	Firstname NVARCHAR(200) NOT NULL,
	Lastname NVARCHAR(200) NOT NULL
);

CREATE TABLE Project0.CustomerOrder (
	
	Id INT IDENTITY(1, 1) NOT NULL,
	CustomerId INT NOT NULL,
	StoreId INT NOT NULL
);

CREATE TABLE Project0.Product (

	Id INT IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	Price FLOAT NOT NULL
);

CREATE TABLE Project0.StoreStock (

	Id INT IDENTITY(1, 1) NOT NULL,
	StoreId INT NOT NULL,
	ProductId INT NOT NULL,
	ProductQuantity INT NOT NULL
);

CREATE TABLE Project0.OrderLine (

	Id INT IDENTITY(1, 1) NOT NULL,
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,
	ProductQuantity INT NOT NULL
);