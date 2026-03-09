----------------------------------------
-- Eg1:  Create a UDF to get sum of numbers
----------------------------------------


--- Creating udf
CREATE FUNCTION dbo.udf_getSum(@x INT,  @y INT)  RETURNS INT
AS
BEGIN
	RETURN(@x + @y);
END;


-- Invoke the udf
SELECT dbo.udf_getSum(10,20)


----------------------------------------------------------
-- Eg2:  Create a UDF to return employee count based on deptno
-------------------------------------------------------------


--- Creating udf
ALTER FUNCTION dbo.udf_getEmpCount(@deptno INT)  RETURNS INT
AS
BEGIN
	DECLARE @count INT	
	SELECT @count = Count(*) FROM Emp WHERE Deptno=@deptno;
	RETURN( @count );
END;


-- Invoke the udf
SELECT dbo.udf_getEmpCount(20);

----------------------------------------------------------
-- Eg3:  Create a UDF to return all employee details based on deptno
-------------------------------------------------------------


--- Creating udf
CREATE FUNCTION dbo.udf_getEmpsByDeptno( @deptno INT )  RETURNS TABLE
AS
RETURN( SELECT * FROM Emps WHERE Deptno=@deptno );



-- Invoke the udf
SELECT * from  dbo.udf_getEmpsByDeptno(20);
----------------------------------------------------------
-- Eg4:  Get the total price of an order (given an OrderID)
-------------------------------------------------------------


use OnlineBookStore

CREATE FUNCTION dbo.fn_GetOrderTotal( @OrderID INT ) RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Total DECIMAL(10,2)

    SELECT @Total = SUM(b.Price * od.Quantity)
    FROM Order_Details od
    JOIN Books b ON od.Book_ID = b.Book_ID
    WHERE od.Order_ID = @OrderID

    RETURN ISNULL(@Total, 0)
END


-- Invoke the udf
SELECT dbo.fn_GetOrderTotal(2) AS OrderTotal





SELECT * FROM Orders
SELECT * FROM Order_Details

DECLARE @OrderID INT = 1
SELECT SUM(b.Price * od.Quantity)
    FROM Order_Details od
    JOIN Books b ON od.Book_ID = b.Book_ID
    WHERE od.Order_ID = @OrderID

	------------------------------------------
--   ************* Working with Triggers  ********************
------------------------------------------------


use EmployeeDb 

SELECT * FROM Emp

DELETE FROM Emp  WHERE Empno = 7113


CREATE TRIGGER trg_InseadOfDelete
ON Emp
INSTEAD OF DELETE
AS
BEGIN
	PRINT 'DELETE operation not allowed on Emps table';	
END;



ALTER TRIGGER trg_InseadOfDelete
ON Emp
INSTEAD OF DELETE
AS
BEGIN
	RAISERROR('DELETE operation not allowed on Emps table', 16, 1);
END;



--  ----------------------------------------------


CREATE TABLE EmpLogTable(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Operation  NVARCHAR(100),
	Empno INT,  
	PerformedOn Date,  
	Description NVARCHAR(200)
)



CREATE TRIGGER  trg_after_insert_emps
ON Emps  
AFTER INSERT
AS  
BEGIN  
		DECLARE @eno INT
		SELECT  @eno = empno from INSERTED; 

	  -- Trigger Statements  
      -- Insert, Update, Or Delete Statements  
		INSERT INTO EmpLogTable VALUES('INSERT Opeation', @eno, GETDATE(), 'This message generated at the time insert a record in emp table')

		Print 'AFTER INSERT TRIGGER IS EXECUTED';
END;



select * from EmpLogTable

SELECT * FROM Emps 

INSERT INTO Emps VALUES(1122,'Smith','LEAD',1600,10);