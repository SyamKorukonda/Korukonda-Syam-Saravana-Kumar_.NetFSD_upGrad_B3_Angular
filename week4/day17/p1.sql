CREATE DATABASE Hand_17;
USE Hand_17;

/*Transactions and Trigger Implementation
Scenario
Auto retail company wants to ensure stock consistency while placing orders.
Whenever an order is placed, stock should reduce automatically and transaction should rollback if stock is insufficient.*/

-- Products Table
CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    stock_qty INT
);

-- Orders Table
CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    order_date DATE,
    order_status INT
);

-- Order Items Table
CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO products VALUES
(1,'Laptop',10),
(2,'Mouse',20),
(3,'Keyboard',15);

SELECT * FROM products;

/*Write a transaction to insert data into orders and order_items tables.
- Check stock availability before confirming order.
- Create a trigger to reduce stock quantity after order insertion.
- Rollback transaction if stock quantity is insufficient.*/


/* A trigger is used to automatically execute SQL code when a specific event occurs (INSERT, UPDATE, DELETE).
In your case, the trigger runs after inserting into order_items. */

--CREATE TRIGGER

	CREATE TRIGGER trg_reduce_stock
	ON order_items
	AFTER INSERT
	AS
	BEGIN

	IF EXISTS(
		SELECT 1
		FROM products p
		JOIN inserted i ON p.product_id = i.product_id
		WHERE p.stock_qty < i.quantity
	)
	BEGIN
		RAISERROR('Insufficient stock',16,1);
		ROLLBACK TRANSACTION;
		RETURN;
	END

	UPDATE p
	SET p.stock_qty = p.stock_qty - i.quantity
	FROM products p
	JOIN inserted i
	ON p.product_id = i.product_id;

	END;

--Test Successful Order
BEGIN TRANSACTION;

INSERT INTO orders VALUES (101,GETDATE(),1);

INSERT INTO order_items VALUES
(10,101,1,2),
(11,101,2,4);

COMMIT TRANSACTION;

--Check Results
SELECT * FROM orders;
SELECT * FROM order_items;
SELECT * FROM products;


--Test Rollback (Insufficient Stock)
	BEGIN TRANSACTION;

	INSERT INTO orders VALUES (103,GETDATE(),1);

	INSERT INTO order_items VALUES
	(13,103,1,2);

	COMMIT TRANSACTION;


--Verify Rollback
SELECT * FROM orders WHERE order_id = 102;
SELECT * FROM order_items WHERE order_id = 102;

--Check Final Stock

SELECT * FROM products;
