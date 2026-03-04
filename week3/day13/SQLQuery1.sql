use Employee;

CREATE TABLE Employees (
    EmpID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50),
    Department VARCHAR(50),
    Salary INT
);

INSERT INTO Employees (Name, Department, Salary)
VALUES
('Ram', 'IT', 50000),
('Ravi', 'HR', 60000),
('Sita', 'IT', 55000),
('Jahnavi', 'Sales', 45000),
('Sam', 'IT', 65000);

select * from Employees;

-- Find the total number of employees.

SELECT COUNT(*) AS Total_employees FROM Employees;

--Find the average salary of employees.

SELECT AVG(Salary) as Average from Employees;

--Find the maximum salary in the company.

SELECT MAX(Salary) AS MaxSalary from Employees;

--Find the minimum salary in the company.

SELECT MIN(Salary) AS MinSalary from Employees;

--Find the total salary paid to all employees.

SELECT SUM(Salary) AS TotalSalary FROM Employees;

-- employ tablee--

CREATE TABLE Emp (
    EmpID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50),
    Department VARCHAR(50),
    Salary INT,
    City VARCHAR(50),
    JoinDate DATE
);	

INSERT INTO Emp (Name, Department, Salary, City, JoinDate)
VALUES
('Ram', 'IT', 50000, 'Hyderabad', '2023-01-10'),
('Ravi', 'HR', 45000, 'Chennai', '2022-05-20'),
('Sita', 'IT', 60000, 'Hyderabad', '2021-03-15'),
('John', 'Sales', 40000, 'Mumbai', '2022-07-11'),
('Sam', 'IT', 55000, 'Bangalore', '2020-02-18'),
('Anu', 'HR', 48000, 'Chennai', '2023-06-05'),
('Raj', 'Sales', 42000, 'Mumbai', '2021-09-22'),
('Priya', 'IT', 65000, 'Bangalore', '2022-11-30'),
('Kiran', 'HR', 47000, 'Hyderabad', '2020-12-01'),
('Neha', 'Sales', 43000, 'Delhi', '2023-04-19');

select * from Emp;

select Department,sum(Salary) as TotalSalary 
from Emp
group by Department
having sum(Salary)>155000;