
--Whenever a new record is inserted into order_items, the stock quantity in the stocks table must automatically decrease based on the ordered quantity.

USE SalesDB;

CREATE TABLE products_inventory (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    stock_qty INT
);

CREATE TABLE customer_orders (
    order_id INT PRIMARY KEY,
    order_date DATE
);

CREATE TABLE order_items (
    item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    ordered_qty INT,
    FOREIGN KEY (order_id) REFERENCES customer_orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products_inventory(product_id)
);

INSERT INTO products_inventory VALUES
	(201,'Keyboard',50),
	(202,'Mouse',40),
	(203,'Monitor',25),
	(204,'Printer',15);

INSERT INTO customer_orders VALUES
	(1,'2024-02-10'),
	(2,'2024-02-12');


-- Create an AFTER INSERT trigger on order_items.
-- Reduce the corresponding quantity in stocks table.
-- Prevent stock from becoming negative.
-- If stock is insufficient, rollback the transaction with a custom error message.

CREATE TRIGGER trg_UpdateStockAfterOrder
ON order_items
AFTER INSERT 
AS 
BEGIN 
	
	BEGIN TRY 

		IF EXISTS ( 
					SELECT 1 
					FROM inserted i
					JOIN products_inventory p ON i.product_id=p.product_id
					WHERE p.stock_qty <i.ordered_qty 
				 )
				 BEGIN 
					RAISERROR('stock insufficient for the order .',16,1)
					ROLLBACK TRANSACTION 
					RETURN 
				END 

				UPDATE p
				SET p.stock_qty = p.stock_qty - i.ordered_qty
				FROM products_inventory p
				JOIN inserted i
				ON p.product_id = i.product_id
	END TRY

	BEGIN CATCH 
		THROW 
	END CATCH 
END 


INSERT INTO order_items VALUES
(1,1,201,5)


INSERT INTO order_items VALUES
(2,1,204,20)