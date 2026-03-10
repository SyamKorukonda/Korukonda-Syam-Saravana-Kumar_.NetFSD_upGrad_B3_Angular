CREATE DATABASE Day17;
USE Day17;

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
    order_date DATE
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

-- Sample Products
INSERT INTO products VALUES
(1,'Laptop',10),
(2,'Mouse',20),
(3,'Keyboard',15);

/*Write a transaction to insert data into orders and order_items tables.
- Check stock availability before confirming order.
- Create a trigger to reduce stock quantity after order insertion.
- Rollback transaction if stock quantity is insufficient.*/

CREATE TRIGGER trg_reduce_stock
ON order_items
AFTER INSERT
AS
BEGIN
    IF EXISTS (
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
    JOIN inserted i ON p.product_id = i.product_id;
END;


-- Transaction for placing order
BEGIN TRANSACTION;

BEGIN TRY

INSERT INTO orders VALUES (101,GETDATE());

INSERT INTO order_items VALUES
(1,101,1,2),
(2,101,2,3);

COMMIT TRANSACTION;

END TRY
BEGIN CATCH

ROLLBACK TRANSACTION;

END CATCH;


SELECT * FROM orders;
SELECT * FROM order_items;
SELECT * FROM products;