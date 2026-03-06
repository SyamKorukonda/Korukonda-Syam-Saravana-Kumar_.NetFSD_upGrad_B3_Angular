use day15;

--You are assigned as a database developer to set up the EcommDb database for an automobile retail company.
-- The company wants to verify basic operations such as inserting data and retrieving product and customer information.
CREATE TABLE categories
(
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);
	CREATE TABLE brands
(
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50)
);
CREATE TABLE products
(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
CREATE TABLE customers
(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    city VARCHAR(50),
    phone VARCHAR(20),
    email VARCHAR(100)
);
CREATE TABLE stores
(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100),
    city VARCHAR(50)
);
INSERT INTO categories VALUES
(1,'Motorcycles'),
(2,'Scooters'),
(3,'Electric Bikes'),
(4,'Sports Bikes'),
(5,'Cruiser Bikes');
INSERT INTO brands VALUES
(1,'Honda'),
(2,'Yamaha'),
(3,'Suzuki'),
(4,'Kawasaki'),
(5,'TVS');
INSERT INTO products VALUES
(1,'Honda Shine',1,1,2023,85000),
(2,'Yamaha R15',2,4,2024,180000),
(3,'Suzuki Access',3,2,2023,90000),
(4,'Kawasaki Ninja',4,4,2024,450000),
(5,'TVS iQube',5,3,2024,120000);
INSERT INTO customers VALUES
(1,'Ram','Kumar','Hyderabad','9876543210','ram@gmail.com'),
(2,'Sam','Reddy','Vijayawada','9876543211','sam@gmail.com'),
(3,'Ravi','Teja','Hyderabad','9876543212','ravi@gmail.com'),
(4,'Anil','Kumar','Guntur','9876543213','anil@gmail.com'),
(5,'Kiran','Rao','Hyderabad','9876543214','kiran@gmail.com');
INSERT INTO stores VALUES
(1,'AutoHub Hyderabad','Hyderabad'),
(2,'BikeWorld Vijayawada','Vijayawada'),
(3,'Speed Motors Guntur','Guntur'),
(4,'Elite Bikes Vizag','Visakhapatnam'),
(5,'Prime Auto Bangalore','Bangalore');

--- Create EcommDb and all tables using the provided schema.
--- Insert at least 5 records in categories, brands, products, customers, and stores.
-- Write SELECT queries to retrieve all products with their brand and category names.

SELECT p.product_name,b.brand_name,c.category_name
FROM products p
JOIN brands b ON p.brand_id=b.brand_id
JOIN categories c ON p.category_id=c.category_id;

-- Retrieve all customers from a specific city.

SELECT CONCAT(c.first_name,' ',C.last_name),C.city
FROM customers c
WHERE city='Vijayawada';

-- Display total number of products available in each category
SELECT c.category_name,COUNT(p.product_id) AS TotalProducts
FROM categories c
JOIN products p ON c.category_id=p.category_id
group by c.category_name;
