
-- creation of database
CREATE DATABASE Employee;

-- database usage
use Employee;

--table creation
create table dept(
 deptNo INT PRIMARY KEY,
 deptName VARCHAR(50) not null,
 deptLocat varchar(50));

 -- inserting values
INSERT INTO DEPT VALUES
(10, 'HR', 'HYDERABAD'),
(20, 'SALES', 'MUMBAI'),
(30, 'IT', 'BANGALORE');

-- data retrival 
 select * from dept;