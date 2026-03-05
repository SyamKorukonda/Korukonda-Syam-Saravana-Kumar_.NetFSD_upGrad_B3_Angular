use Day14;
--The company wants to classify customers based on their total order value and identify customers who have placed orders versus those who have not.
CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);
CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_value INT
);
INSERT INTO customers VALUES
	(1,'John','Smith'),
	(2,'David','Lee'),
	(3,'Mike','Brown'),
	(4,'Sara','Wilson');
INSERT INTO orders VALUES
	(101,1,7000),
	(102,1,6000),
	(103,2,4000),
	(104,2,3500),
	(105,3,2000);
	SELECT * FROM customers;
	SELECT * FROM orders;
--1. Use nested query to calculate total order value per customer.
--2. Classify customers using conditional logic:
--- 'Premium' if total order value > 10000
--- 'Regular' if total order value between 5000 and 10000
--- 'Basic' if total order value < 5000
--3. Use UNION to display customers with orders and customers without orders.
--4. Display full name using string concatenation.
--5. Handle NULL cases appropriately

SELECT 
CONCAT(c.first_name,' ',c.last_name) AS customer_name,
SUM(o.order_value) AS total_value,
CASE 
    WHEN SUM(o.order_value) > 10000 THEN 'Premium'
    WHEN SUM(o.order_value) BETWEEN 5000 AND 10000 THEN 'Regular'
    ELSE 'Basic'
END AS customer_category
FROM customers c
JOIN orders o
ON c.customer_id = o.customer_id
GROUP BY c.first_name, c.last_name

UNION

SELECT 
    CONCAT(first_name,' ',last_name) AS customer_name,
    0 AS total_value,
    'No Orders' AS customer_category
FROM customers
WHERE customer_id NOT IN (
        SELECT customer_id FROM orders
);