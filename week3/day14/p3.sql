--Management wants to evaluate store performance by identifying stores that have sold products but currently have zero stock for those products.

use Day14;
--1. Identify products sold in each store using nested queries.
--2. Compare sold products with current stock using INTERSECT and EXCEPT operators.
--3. Display store_name, product_name, total quantity sold.
--4. Calculate total revenue per product (quantity × list_price – discount).
--5. Update stock quantity to 0 for discontinued products (simulation).

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(50)
);

CREATE TABLE products1 (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(50),
    list_price INT
);
CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    store_id INT
);

CREATE TABLE order_items (
    order_id INT,
    product_id INT,
    quantity INT,
    discount INT
);
CREATE TABLE stocks (
    store_id INT,
    product_id INT,
    quantity INT
);
INSERT INTO stores VALUES
(1,'Hyderabad Store'),
(2,'Delhi Store');

INSERT INTO products1 VALUES
(101,'Mountain Bike',10000),
(102,'Road Bike',8000),
(103,'Electric Bike',15000);

INSERT INTO orders VALUES
(1,1),
(2,1),
(3,2);

INSERT INTO order_items VALUES
(1,101,2,500),
(2,102,1,200),
(3,101,1,500),
(3,103,2,1000);

INSERT INTO stocks VALUES
(1,101,0),
(1,102,5),
(2,101,0),
(2,103,0);

SELECT * FROM stores ;
SELECT * FROM  stocks;
SELECT * FROM  order_items; 
SELECT * FROM  orders;
SELECT * FROM  products1;

SELECT 
s.store_name,
p.product_name,
SUM(oi.quantity) AS total_quantity_sold,
SUM((oi.quantity * p.list_price) - oi.discount) AS total_revenue
FROM stores s
JOIN orders o ON s.store_id = o.store_id
JOIN order_items oi ON o.order_id = oi.order_id
JOIN products1 p ON oi.product_id = p.product_id
WHERE (s.store_id IN (
        SELECT o.store_id
        FROM orders o
        JOIN order_items oi ON o.order_id = oi.order_id
        INTERSECT
        SELECT store_id
        FROM stocks
        WHERE quantity = 0
))
GROUP BY s.store_name, p.product_name;