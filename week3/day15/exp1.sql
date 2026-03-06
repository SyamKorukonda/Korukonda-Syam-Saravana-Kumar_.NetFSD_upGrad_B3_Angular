CREATE DATABASE day15;
use day15;
CREATE TABLE Emp
(
    EmpId INT,
    Ename VARCHAR(50),
    Deptno INT,
    Salary INT,
    Email VARCHAR(100)
);

INSERT INTO Emp VALUES
	(1,'Ram',10,40000,'ram@gmail.com'),
	(2,'Sam',20,45000,'sam@gmail.com'),
	(3,'Ravi',10,50000,'ravi@gmail.com'),
	(4,'Anil',30,42000,'anil@gmail.com'),
	(5,'Kiran',20,47000,'kiran@gmail.com'),
	(6,'John',30,52000,'john@gmail.com');

--Clustered Index Creation
 CREATE CLUSTERED INDEX idx_empid
 ON Emp(EmpId);

 -- NON -CLUSTERED INDEX CREATION
 CREATE NONCLUSTERED INDEX idx_ename ON Emp(Ename); 

 -- UNIQUE INDEX CREATION 
 CREATE UNIQUE INDEX idx_email ON Emp(Email);

 --View all indexs
 EXEC sp_helpindex 'Emp';

--Reorganize Index
ALTER INDEX ALL ON Emp REORGANIZE;
ALTER INDEX idx_ename ON Emp REORGANIZE;

--Rebuild Index
ALTER INDEX ALL ON Emp REBUILD;
ALTER INDEX idx_ename ON Emp REBUILD;

--Disable Index
ALTER INDEX idx_ename ON Emp DISABLE;

--Enable Disabled Index
ALTER INDEX idx_ename ON Emp REBUILD;

--Drop Index
DROP INDEX idx_ename ON Emp;

