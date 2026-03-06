use day15;


CREATE TABLE categories1
(
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);
	CREATE TABLE brands1
(
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50)
);
CREATE TABLE products1
(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),
    FOREIGN KEY (brand_id) REFERENCES brands1(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories1(category_id)
);
CREATE TABLE customers1
(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    city VARCHAR(50),
    phone VARCHAR(20),
    email VARCHAR(100)
);
CREATE TABLE stores1
(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100),
    city VARCHAR(50)
);
CREATE TABLE staffs1
(
    staff_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100),
    store_id INT,
    FOREIGN KEY (store_id) REFERENCES stores1(store_id)
);
CREATE TABLE orders1
(
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_status INT,
    order_date DATE,
    store_id INT,
    staff_id INT,
    FOREIGN KEY (customer_id) REFERENCES customers1(customer_id),
    FOREIGN KEY (store_id) REFERENCES stores1(store_id),
    FOREIGN KEY (staff_id) REFERENCES staffs1(staff_id)
);
INSERT INTO categories1 VALUES
(1,'Motorcycles'),
(2,'Scooters'),
(3,'Electric Bikes'),
(4,'Sports Bikes'),
(5,'Cruiser Bikes');
INSERT INTO brands1 VALUES
(1,'Honda'),
(2,'Yamaha'),
(3,'Suzuki'),
(4,'Kawasaki'),
(5,'TVS');
INSERT INTO products1 VALUES
(1,'Honda Shine',1,1,2023,85000),
(2,'Yamaha R15',2,4,2024,180000),
(3,'Suzuki Access',3,2,2023,90000),
(4,'Kawasaki Ninja',4,4,2024,450000),
(5,'TVS iQube',5,3,2024,120000);
INSERT INTO customers1 VALUES
(1,'Ram','Kumar','Hyderabad','9876543210','ram@gmail.com'),
(2,'Sam','Reddy','Vijayawada','9876543211','sam@gmail.com'),
(3,'Ravi','Teja','Hyderabad','9876543212','ravi@gmail.com'),
(4,'Anil','Kumar','Guntur','9876543213','anil@gmail.com'),
(5,'Kiran','Rao','Hyderabad','9876543214','kiran@gmail.com');
INSERT INTO stores1 VALUES
(1,'AutoHub Hyderabad','Hyderabad'),
(2,'BikeWorld Vijayawada','Vijayawada'),
(3,'Speed Motors Guntur','Guntur'),
(4,'Elite Bikes Vizag','Visakhapatnam'),
(5,'Prime Auto Bangalore','Bangalore');

INSERT INTO staffs1 VALUES
(1,'Raj','Kumar','raj@gmail.com',1),
(2,'Arjun','Reddy','arjun@gmail.com',2),
(3,'David','John','david@gmail.com',3),
(4,'Suresh','Naidu','suresh@gmail.com',4),
(5,'Manoj','Rao','manoj@gmail.com',5);
INSERT INTO orders1 VALUES
(1,1,1,'2024-01-10',1,1),
(2,2,1,'2024-02-05',2,2),
(3,3,1,'2024-03-12',3,3),
(4,4,1,'2024-04-20',4,4),
(5,5,1,'2024-05-15',5,5);
--The management team frequently accesses product and order summary reports. To simplify access and improve performance, they require database views and indexing.

-- Create a view that shows product name, brand name, category name, model year and list price.

CREATE VIEW  ProductDetails AS 
	SELECT p.product_name,b.brand_name,c.category_name,p.model_year,p.list_price
		FROM  products1 p
		JOIN categories1 c ON c.category_id=p.category_id
		JOIN brands1 b ON b.brand_id=p.brand_id;

	SELECT * FROM ProductDetails;

	--DROP VIEW orderDetails;
		
-- Create a view that shows order details with customer name, store name and staff name.

CREATE VIEW viw_orderDetais AS 
SELECT c.first_name+''+c.last_name AS CustomerName,
		s.store_name,
		s1.first_name+''+s1.last_name AS StaffName
FROM customers1 c
JOIN stores1 s ON c.city=s.city 
JOIN staffs1 s1 ON s.store_id=s1.store_id;

SELECT * FROM viw_orderDetais;

-- Create appropriate indexes on foreign key columns.

CREATE NONCLUSTERED INDEX idx_orders_customer
ON orders1(customer_id);

CREATE NONCLUSTERED INDEX idx_orders_store
ON orders1(store_id);

CREATE NONCLUSTERED INDEX idx_orders_staff
ON orders1(staff_id);

-- Test performance improvement using execution plan


