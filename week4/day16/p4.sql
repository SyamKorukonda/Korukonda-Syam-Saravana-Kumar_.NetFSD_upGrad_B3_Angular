
--Cursor-Based Revenue Calculation Scenario
--Management wants a detailed revenue calculation per store by iterating through completed orders and calculating total revenue including discounts.

use SalesDB;

CREATE TABLE shop_locations (
    shop_id INT PRIMARY KEY,
    shop_name VARCHAR(100)
);

CREATE TABLE sales_orders (
    order_id INT PRIMARY KEY,
    shop_id INT,
    order_status INT,
    order_date DATE,
    FOREIGN KEY (shop_id) REFERENCES shop_locations(shop_id)
);

CREATE TABLE sales_items (
    item_id INT PRIMARY KEY,
    order_id INT,
    product_name VARCHAR(100),
    price DECIMAL(10,2),
    quantity INT,
    discount DECIMAL(5,2),
    FOREIGN KEY (order_id) REFERENCES sales_orders(order_id)
);
INSERT INTO shop_locations VALUES
(1,'Delhi Store'),
(2,'Mumbai Store'),
(3,'Pune Store');

INSERT INTO sales_orders VALUES
(101,1,4,'2024-04-01'),
(102,2,4,'2024-04-03'),
(103,1,2,'2024-04-05'),
(104,3,4,'2024-04-06');

INSERT INTO sales_items VALUES
(1,101,'Laptop',60000,1,5),
(2,101,'Mouse',1500,2,0),
(3,102,'Tablet',20000,1,10),
(4,104,'Printer',12000,1,5),
(5,104,'Keyboard',2000,2,0);

-- Use a cursor to iterate through completed orders (order_status = 4).
-- Calculate total revenue per order using order_items.
-- Store computed revenue in a temporary table.
--Display store-wise revenue summary.

BEGIN TRY

BEGIN TRANSACTION

CREATE TABLE #RevenueTemp (
    shop_id INT,
    order_id INT,
    revenue DECIMAL(12,2)
);

DECLARE @order_id INT
DECLARE @shop_id INT
DECLARE @revenue DECIMAL(12,2)

DECLARE order_cursor CURSOR FOR
SELECT order_id, shop_id
FROM sales_orders
WHERE order_status = 4

OPEN order_cursor

FETCH NEXT FROM order_cursor INTO @order_id, @shop_id

WHILE @@FETCH_STATUS = 0
BEGIN

    SELECT @revenue =
    SUM((price * quantity) - ((price * quantity) * ISNULL(discount,0)/100))
    FROM sales_items
    WHERE order_id = @order_id

    INSERT INTO #RevenueTemp
    VALUES(@shop_id, @order_id, @revenue)

    FETCH NEXT FROM order_cursor INTO @order_id, @shop_id
END

CLOSE order_cursor
DEALLOCATE order_cursor

SELECT shop_id, SUM(revenue) AS TotalRevenue
FROM #RevenueTemp
GROUP BY shop_id

COMMIT TRANSACTION

END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
PRINT 'Error occurred during revenue calculation'
END CATCH
