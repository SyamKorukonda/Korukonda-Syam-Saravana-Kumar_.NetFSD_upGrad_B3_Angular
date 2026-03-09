
--The company requires reusable database logic to generate reports such as total sales per store and discounted order totals.

CREATE DATABASE SalesDB;

USE SalesDB;

CREATE TABLE Stores (
    StoreID INT PRIMARY KEY,
    StoreName VARCHAR(100)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Price DECIMAL(10,2)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    StoreID INT,
    OrderDate DATE,
    FOREIGN KEY (StoreID) REFERENCES Stores(StoreID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    Discount DECIMAL(5,2),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

INSERT INTO Stores VALUES
	(1,'Hyderabad Store'),
	(2,'Chennai Store'),
	(3,'Bangalore Store');

INSERT INTO Products VALUES
	(101,'Laptop',50000),
	(102,'Mobile',20000),
	(103,'Tablet',15000),
	(104,'Headphones',2000),
	(105,'Smart Watch',8000);

INSERT INTO Orders VALUES
	(1,1,'2024-01-10'),
	(2,2,'2024-01-12'),
	(3,1,'2024-01-15'),
	(4,3,'2024-01-18');

INSERT INTO OrderDetails VALUES
	(1,1,101,1,5),
	(2,1,104,2,0),
	(3,2,102,1,10),
	(4,3,105,3,5),
	(5,4,103,2,0),
	(6,4,102,1,5);

-- Create a stored procedure to generate total sales amount per store.

CREATE PROCEDURE sp_Totalsalesperstore
	@StoreID INT
AS
BEGIN 
	SELECT 
		S.StoreName, SUM(P.Price * OD.Quantity) AS TotalSales
		FROM Orders O
		JOIN Stores S ON O.StoreID=S.StoreID
		JOIN OrderDetails OD  ON OD.OrderID=O.OrderID
		JOIN Products P ON P.ProductID=OD.ProductID
		WHERE O.StoreID=@StoreID
		GROUP BY S.StoreName
END

--Execute Procedures

EXEC sp_TotalSalesPerStore 1;
------------------------------------------------------------------------------------------------------------------------------


-- Create a stored procedure to retrieve orders by date range.

CREATE PROCEDURE sp_GetOrdersByDateRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT *
    FROM Orders
    WHERE OrderDate BETWEEN @StartDate AND @EndDate
END

--Execute Procedures

EXEC sp_GetOrdersByDateRange '2024-01-01','2024-01-20';
-------------------------------------------------------------------------------------------------------------------------------------------

-- Create a scalar function to calculate total price after discount.
CREATE FUNCTION fn_CalculateDiscountPrice
(
    @Price DECIMAL(10,2),
    @Quantity INT,
    @Discount DECIMAL(5,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN (@Price * @Quantity) - ((@Price * @Quantity) * ISNULL(@Discount,0) / 100)
END

--Call Functions

SELECT dbo.fn_CalculateDiscountPrice(50000,1,10) AS DiscountPrice;

-----------------------------------------------------------------------------------------------------------------
-- Create a table-valued function to return top 5 selling products.

CREATE FUNCTION fn_TopSellingProducts()
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 5
        P.ProductID,
        P.ProductName,
        SUM(OD.Quantity) AS TotalSold
    FROM OrderDetails OD
    JOIN Products P ON OD.ProductID = P.ProductID
    GROUP BY P.ProductID, P.ProductName
    ORDER BY TotalSold DESC
)

--Call Functions
SELECT * FROM dbo.fn_TopSellingProducts();
-------------------------------------------------------------------------------------------------------------------------





