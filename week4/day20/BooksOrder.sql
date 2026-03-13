

CREATE DATABASE CaseStudy;
USE CaseStudy;

CREATE TABLE Books (
    BookID  INT IDENTITY(1,1) PRIMARY KEY,
    Title   NVARCHAR(150) NOT NULL,
    Stock   INT NOT NULL CHECK (Stock >= 0),
    Price   DECIMAL(10,2) NOT NULL
);

CREATE TABLE Orders (
    OrderID    INT IDENTITY(1,1) PRIMARY KEY,
    BookID     INT NOT NULL,
    Quantity   INT NOT NULL CHECK (Quantity > 0),
    OrderDate  DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

--Task 1: Stored Procedure to Add a Book  
--Create a stored procedure named sp_AddNewBook that takes: @Title NVARCHAR(150), @Stock INT, @Price DECIMAL(10,2)

--A Stored Procedure is a prewritten SQL program stored in the database that can be executed whenever needed.
--It contains SQL statements like SELECT, INSERT, UPDATE, DELETE.


CREATE PROCEDURE sp_AddNewBook
@Titles NVARCHAR(150),
@Stock INT,
@Price DECIMAL(10,2)

AS
BEGIN

BEGIN  TRY

 INSERT INTO Books(Title,Stock,Price) VALUES
 (@Titles,@Stock,@Price)

 PRINT 'Book added successfully'

END TRY

BEGIN CATCH
PRINT 'Error occur while adding book'
END CATCH 

END

--Task 2: Main Stored Procedure – Place Order with Transaction  
--Create a stored procedure named sp_PlaceOrder with parameters: @BookID INT, @Quantity INT



CREATE PROCEDURE sp_PlaceOrder
@BookID INT,
@Quantity INT 

AS
BEGIN --start of the procedure code block
SET XACT_ABORT ON  --If any runtime error occurs, SQL Server automatically rolls back the transaction.--Without this : partial updates may occur

BEGIN TRY --error handling block.

BEGIN TRANSACTION   --Multiple SQL operations behave like one single unit.

IF NOT EXISTS(
	SELECT * FROM Books
	WHERE BookID=@BookID AND Stock>=@Quantity)

	RAISERROR('Not enough stock or book not found.',16,1)

	UPDATE Books
	SET Stock = Stock - @Quantity
	WHERE BookID=@BookID

	INSERT INTO Orders(BookID,Quantity)
	VALUES(@BookID,@Quantity)
	COMMIT TRANSACTION
	
	PRINT 'Order placed successfully.'

END TRY

BEGIN CATCH
	IF @@TRANCOUNT > 0  --Check if a transaction is currently active.
	ROLLBACK TRANSACTION

	PRINT 'Error ' + CAST(ERROR_NUMBER() AS VARCHAR)
	+ ': ' + ERROR_MESSAGE()

END CATCH

END

--Insert Sample Data
EXEC sp_AddNewBook 'C# Programming',10,500
EXEC sp_AddNewBook 'SQL Server Basics',5,400
EXEC sp_AddNewBook 'Python Guide',8,450


--Test Cases
EXEC sp_PlaceOrder 1,2     --Order placed successfully.
EXEC sp_PlaceOrder 2,10	   --Not enough stock or book not found.
EXEC sp_PlaceOrder 100,1    --Not enough stock or book not found.




