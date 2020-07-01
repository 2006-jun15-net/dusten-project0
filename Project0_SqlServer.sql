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
	Timestamp DATETIME NOT NULL,
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

ALTER TABLE Business.OrderLine 
	ADD CONSTRAINT FK_OrderLine_CustomerOrder_OrderId FOREIGN KEY (OrderId)
	REFERENCES Business.CustomerOrder (Id) ON DELETE CASCADE;

GO
CREATE TRIGGER Business.OnCustomerOrderDelete ON Business.CustomerOrder
FOR DELETE
AS
BEGIN
	DELETE Business.OrderLine WHERE OrderId IN (SELECT Id FROM Deleted);
END

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
INSERT INTO Business.Store (Name) VALUES ('Dairy Mart'); -- 1
INSERT INTO Business.Store (Name) VALUES ('Escalona Mart'); -- 2

-- Customers
INSERT INTO Business.Customer (StoreId, Firstname, Lastname) VALUES (1, 'John', 'Smith'); -- 1
INSERT INTO Business.Customer (StoreId, Firstname, Lastname) VALUES (1, 'Thomas', 'Anderson'); -- 2
INSERT INTO Business.Customer (StoreId, Firstname, Lastname) VALUES (1, 'Agent', 'Smith'); -- 3

-- Products
INSERT INTO Business.Product (Name, Price) VALUES ('Milk', 1.5); -- 1
INSERT INTO Business.Product (Name, Price) VALUES ('Cheese', 2.0); -- 2
INSERT INTO Business.Product (Name, Price) VALUES ('Pie', 5.0); -- 3
INSERT INTO Business.Product (Name, Price) VALUES ('Coffee', 1.0); -- 4

-- StoreStocks
INSERT INTO Business.StoreStock (StoreId, ProductId, ProductQuantity) VALUES (1, 1, 50); -- 1
INSERT INTO Business.StoreStock (StoreId, ProductId, ProductQuantity) VALUES (1, 2, 50); -- 2
INSERT INTO Business.StoreStock (StoreId, ProductId, ProductQuantity) VALUES (2, 4, 100); -- 3

-- CustomerOrders
INSERT INTO Business.CustomerOrder (CustomerId, StoreId, Timestamp) VALUES (1, 1, '2020-06-28 00:00:00'); -- 1
INSERT INTO Business.CustomerOrder (CustomerId, StoreId, Timestamp) VALUES (3, 2, '2020-06-30 00:00:00'); -- 2
INSERT INTO Business.CustomerOrder (CustomerId, StoreId, Timestamp) VALUES (2, 1, '2020-06-28 00:00:00'); -- 3
INSERT INTO Business.CustomerOrder (CustomerId, StoreId, Timestamp) VALUES (1, 2, '2020-06-29 00:00:00'); -- 4

-- OrderLines
INSERT INTO Business.OrderLine (OrderId, ProductId, ProductQuantity) VALUES (1, 1, 2); -- 1
INSERT INTO Business.OrderLine (OrderId, ProductId, ProductQuantity) VALUES (2, 4, 12); -- 2
INSERT INTO Business.OrderLine (OrderId, ProductId, ProductQuantity) VALUES (3, 2, 3); -- 3
INSERT INTO Business.OrderLine (OrderId, ProductId, ProductQuantity) VALUES (3, 1, 3); -- 4
INSERT INTO Business.OrderLine (OrderId, ProductId, ProductQuantity) VALUES (4, 4, 10); -- 5

SELECT * FROM Business.Store;
SELECT * FROM Business.Customer;
SELECT * FROM Business.Product;
SELECT * FROM Business.StoreStock;
SELECT * FROM Business.CustomerOrder;
SELECT * FROM Business.OrderLine;
