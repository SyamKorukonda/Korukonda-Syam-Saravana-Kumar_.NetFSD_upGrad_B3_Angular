
CREATE DATABASE HandsOn; 

use HandsOn;

--The store manager wants a simple report showing customer orders 
--along with their order dates and status. This report will help track pending and completed orders.

CREATE TABLE Customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);
CREATE TABLE Orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_date DATE,
    order_status INT,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);

INSERT INTO Customers VALUES
	(1, 'Ram', 'Kumar'),
	(2, 'Ravi', 'Sharma'),
	(3, 'Sita', 'Reddy'),
	(4, 'John', 'David');

INSERT INTO Orders VALUES
	(101, 1, '2024-03-10', 1),
	(102, 2, '2024-03-11', 4),
	(103, 3, '2024-03-12', 2),
	(104, 1, '2024-03-13', 4),
	(105, 4, '2024-03-14', 1);

	SELECT * FROM Customers;
	SELECT * FROM Orders;

--1. Retrieve customer first name, last name, order_id, order_date, and order_status.
--2. Display only orders with status Pending (1) or Completed (4).
--3. Sort the results by order_date in descending order

SELECT c.first_name,c.last_name,o.order_id,o.order_date,o.order_status
FROM Customers c
INNER JOIN Orders o 
ON c.Customer_id=o.customer_id 
where o.order_status=1 or o.order_status=4
ORDER BY  o.order_date DESC;
