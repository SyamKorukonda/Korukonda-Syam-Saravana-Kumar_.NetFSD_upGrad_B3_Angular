CREATE DATABASE  Day14; 

use Day14;

--You are working as a database developer for an automobile retail company. 
--Management wants to identify products that are priced higher than the average price of products in their respective categories.
CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);
CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    model_year INT,
    list_price DECIMAL(10,2),
    category_id INT,
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
INSERT INTO categories VALUES
	(1,'Mountain Bikes'),
	(2,'Road Bikes'),
	(3,'Electric Bikes');
INSERT INTO products VALUES
	(1,'Trail Bike',2018,800,1),
	(2,'Mountain Pro',2019,1200,1),
	(3,'Hill Rider',2017,900,1),

	(4,'Speedster',2018,1500,2),
	(5,'Road Master',2019,1800,2),
	(6,'City Rider',2017,1300,2),

	(7,'Electric Flash',2019,2500,3),
	(8,'Eco Bike',2018,2200,3),
	(9,'Power Ride',2017,2700,3);

	SELECT * FROM categories;
	SELECT * FROM products;

--1. Retrieve product details (product_name, model_year, list_price).
--2. Compare each product’s price with the average price of products in the same category using a nested query.
--3. Display only those products whose price is greater than the category average.
--4. Show calculated difference between product price and category average.
--5. Concatenate product name and model year as a single column (e.g., 'ProductName (2017)')


SELECT 
CONCAT(product_name,'(' ,model_year ,')') AS ProducDetails,
list_price-(SELECT AVG(list_price) FROM products p2
								   WHERE p2.category_id=p1.category_id) AS price_difference
FROM products p1
WHERE list_price > (
    SELECT AVG(list_price)
    FROM products p2
    WHERE p2.category_id = p1.category_id
);