SQL Server- Working with Stored Procedure
----------------------------------


	use EmployeeDb;
	
	-- Eg1  Requirement:  Generate Employee count
	CREATE PROCEDURE  usp_GetEmpCount
	AS
	BEGIN
		SELECT COUNT(*) FROM Emp;

		Print('Get Emp Count procedure invoked');
	END;


	-- Executing the SP
	EXECUTE usp_GetEmpCount 
	EXEC usp_GetEmpCount 

	-------------------------------------


	-- Eg2:  Display Employee details by Deptno
	CREATE  PROCEDURE  usp_getEmpsByDeptno
	   @param_deptno INT  	
	AS
	BEGIN
		SELECT * FROM Emp WHERE Deptno=@param_deptno;
	END;


	-- Passing value as input parameter
	EXEC usp_getEmpsByDeptno 20

	DECLARE  @dno INT = 10
	EXEC usp_getEmpsByDeptno @dno
----------------------------------------------------

-- Eg3: Get the no. of employees working in the given deptno

	ALTER  PROCEDURE  usp_getEmpsCountByDeptno
		@param_deptno INT,
		@param_empCount  INT  OUTPUT
	AS
	BEGIN
			DECLARE @dname VARCHAR(100);
			SELECT @dname = Dname from Dept WHERE Deptno=@param_deptno; 

			SELECT @param_empCount=  Count(*) FROM Emp WHERE Deptno=@param_deptno;

		      
     Print ('No. of Employees working in ' + @dname + ' department : '+  CAST(@param_empCount AS VARCHAR));
   
	END;


	-- Execution
	DECLARE  @dno INT =30;
	DECLARE  @empCount INT;
	EXEC usp_getEmpsCountByDeptno @dno, @empCount OUTPUT
	SELECT @empCount

	------------------------------------------
	-- Eg4: Procedure to insert a record in dept table   (includes Error Handling)
	--------------------------------------------------

	ALTER PROCEDURE  usp_insertDeptDetails   
	AS
	BEGIN
		BEGIN TRY
			INSERT INTO Dept VALUES(70, 'IT','Mumbai');	

			Print('New Department created successfully.');
		END TRY

		BEGIN CATCH
				DECLARE @ErrorMessage NVARCHAR(4000);
				DECLARE @ErrorSeverity INT;
				DECLARE @ErrorState INT;

				SELECT  @ErrorMessage = ERROR_MESSAGE(),
						@ErrorSeverity = ERROR_SEVERITY(),
						@ErrorState = ERROR_STATE();

				-- RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);

				-- Prepare INSERT command to log the error details in another error log table 
				PRINT('INSERT OPERATION FAILED')
				PRINT(@ErrorMessage)
		END CATCH
	END;


	EXEC usp_insertDeptDetails

	SELECT * FROM Dept
	
	DROP PROCEDURE usp_insertDeptDetails

--------------------------------------------------
---   Eg5:   Passing TVP(Table Valued Parameter) in Stored Procedure
-----------------------------------------------
		

---  User Defined Type As TABLE 	
CREATE TYPE MyUD_Emp_Table AS TABLE  
(  
    Empno INT PRIMARY KEY,  
    Ename VARCHAR(50),  
    Job  VARCHAR(50),
	Salary DECIMAL(10,2),
	Deptno  INT	
) 



DROP TYPE MyUD_Emp_Table
	



CREATE PROCEDURE usp_insertTableTypeData 
     @myData MyUD_Emp_Table READONLY 
As
BEGIN
	INSERT INTO Emps
	SELECT * FROM  @myData
END;

DROP PROCEDURE usp_insertTableTypeData;


-- Declare variables of type MyUD_Emp_Table
DECLARE @MyEmpTable MyUD_Emp_Table;

-- INSERT data into Table Valued Parameter
INSERT INTO @MyEmpTable VALUES (2222, 'Scott', 'Lead',5000,10)		

Exec usp_insertTableTypeData @MyEmpTable


SELECT * FROM Emps