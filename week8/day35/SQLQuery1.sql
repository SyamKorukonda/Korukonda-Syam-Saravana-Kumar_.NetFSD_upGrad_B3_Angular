	

CREATE DATABASE ContactDb;

USE ContactDb;

CREATE TABLE Company (

CompanyId INT IDENTITY PRIMARY KEY,

CompanyName NVARCHAR(100)

);

CREATE TABLE Department (

DepartmentId INT IDENTITY PRIMARY KEY,

DepartmentName NVARCHAR(100)

);

CREATE TABLE ContactInfo (

ContactId INT IDENTITY PRIMARY KEY,

FirstName NVARCHAR(50),

LastName NVARCHAR(50),

EmailId NVARCHAR(100),

MobileNo BIGINT,

Designation NVARCHAR(50),

CompanyId INT,

DepartmentId INT,

FOREIGN KEY (CompanyId) REFERENCES Company(CompanyId),

FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)

);

INSERT INTO Company (CompanyName) VALUES 
('TCS'),
('Infosys'),
('Wipro'),
('HCL');

INSERT INTO Department (DepartmentName) VALUES 
('HR'),
('IT'),
('Finance'),
('Marketing');

INSERT INTO ContactInfo 
(FirstName, LastName, EmailId, MobileNo, Designation, CompanyId, DepartmentId)
VALUES
('Syam', 'Kumar', 'syam@gmail.com', 9876543210, 'Developer', 1, 2),
('Ravi', 'Teja', 'ravi@gmail.com', 9123456780, 'HR Manager', 2, 1),
('Anil', 'Reddy', 'anil@gmail.com', 9012345678, 'Analyst', 3, 3),
('Priya', 'Sharma', 'priya@gmail.com', 9988776655, 'Marketing Lead', 4, 4);

SELECT * FROM Company;
SELECT * FROM Department;
SELECT * FROM ContactInfo;