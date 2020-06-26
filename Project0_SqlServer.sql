CREATE SCHEMA Business;
GO

CREATE TABLE Business.Store (

	Id INT IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_Store_Id PRIMARY KEY (Id)
);

CREATE TABLE Business.Customer (

	Id INT IDENTITY(1, 1) NOT NULL, 
	StoreId INT NULL,
	Firstname NVARCHAR(200) NOT NULL,
	Lastname NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_Customer_Id PRIMARY KEY (Id)
);

CREATE TABLE Business.CustomerOrder (
	
	Id INT IDENTITY(1, 1) NOT NULL,
	CustomerId INT NOT NULL,
	StoreId INT NOT NULL,
	CONSTRAINT PK_CustomerOrder_Id PRIMARY KEY (Id)
);

CREATE TABLE Business.Product (

	Id INT IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	Price FLOAT NOT NULL,
	CONSTRAINT PK_Product_Id PRIMARY KEY (Id)
);

CREATE TABLE Business.StoreStock (

	Id INT IDENTITY(1, 1) NOT NULL,
	StoreId INT NOT NULL,
	ProductId INT NOT NULL,
	ProductQuantity INT NOT NULL,
	CONSTRAINT PK_StoreStock_Id PRIMARY KEY (Id)
);

CREATE TABLE Business.OrderLine (

	Id INT IDENTITY(1, 1) NOT NULL,
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,
	ProductQuantity INT NOT NULL,
	CONSTRAINT PK_OrderLine_Id PRIMARY KEY (Id)
);

GO
CREATE TRIGGER Business.OnCustomerDelete ON Business.Customer
FOR DELETE
AS
BEGIN
	DELETE Business.CustomerOrder WHERE CustomerId IN (SELECT Id FROM Deleted);
END

GO
CREATE TRIGGER Business.OnStoreDelete ON Business.Store
INSTEAD OF DELETE
AS
BEGIN
	RAISERROR('Deletes not allowed', 15, 1);
END

GO
CREATE TRIGGER Business.OnProductDelete ON Business.Product
INSTEAD OF DELETE
AS
BEGIN
	RAISERROR('Deletes not allowed', 15, 1);
END

-- Stores
INSERT INTO Business.Store (Name) VALUES ('Milk and Cheese');

-- Customers
INSERT INTO Business.Customer (StoreId, Firstname, Lastname) VALUES (1, 'John', 'Smith');

-- Products
INSERT INTO Business.Product (Name, Price) VALUES ('Milk', 1.5);
INSERT INTO Business.Product (Name, Price) VALUES ('Cheese', 2.0);

-- StoreStock
INSERT INTO Business.StoreStock (StoreId, ProductId, ProductQuantity) VALUES (1, 1, 50);
INSERT INTO Business.StoreStock (StoreId, ProductId, ProductQuantity) VALUES (1, 2, 50);

SELECT * FROM Business.Store;
SELECT * FROM Business.Customer;
SELECT * FROM Business.Product;
SELECT * FROM Business.StoreStock;