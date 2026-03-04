
--The inventory manager wants to compare stock availability and total quantity sold for each product

USE HandsOn;

CREATE TABLE Products1 (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100)
);

CREATE TABLE Stores1 (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(50)
);

CREATE TABLE Stocks1 (
    store_id INT,
    product_id INT,
    quantity INT,
    PRIMARY KEY (store_id, product_id),
    FOREIGN KEY (store_id) REFERENCES Stores1(store_id),
    FOREIGN KEY (product_id) REFERENCES Products1(product_id)
);
CREATE TABLE Order_Items1 (
    order_item_id INT PRIMARY KEY,
    product_id INT,
    store_id INT,
    quantity INT
);

INSERT INTO Products1 VALUES
	(1,'Laptop'),
	(2,'Mobile'),
	(3,'Tablet');

INSERT INTO Stores1 VALUES
	(1,'Hyderabad Store'),
	(2,'Chennai Store');

INSERT INTO Stocks1 VALUES
	(1,1,50),
	(1,2,30),
	(2,1,20),
	(2,3,40);

INSERT INTO Order_Items1 VALUES
	(1,1,1,5),
	(2,1,1,3),
	(3,2,1,2),
	(4,1,2,4);
 
 SELECT * FROM Products1;
 SELECT * FROM Stores1;
 SELECT * FROM Stocks1 ;
 SELECT * FROM Order_Items1;
 --1. Display product_name, store_name, available stock quantity, and total quantity sold.
--2. Include products even if they have not been sold (use appropriate join).3. Group results by product_name and store_name.
--4. Sort results by product_name.

SELECT p.product_name,s.store_name,st.quantity AS available_Stock,SUM(oi.quantity) AS total_sold_quantity
FROM Stocks1 st 
JOIN Products1 p ON st.product_id=p.product_id
JOIN Stores1 s ON  s.store_id=st.store_id
LEFT JOIN Order_Items1 oi ON st.product_id=oi.product_id 
						AND st.store_id=s.store_id
GROUP BY p.product_name ,s.store_name,st.quantity
ORDER BY p.product_name;