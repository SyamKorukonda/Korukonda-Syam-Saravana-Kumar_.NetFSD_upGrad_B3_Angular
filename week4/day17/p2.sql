/*Problem 2: Atomic Order Cancellation with SAVEPOINT
Scenario
When cancelling an order, system must restore stock quantities and update order_status to Rejected (3). All actions must be atomic  */
USE Day17;

ALTER TABLE orders
ADD order_status INT;

UPDATE orders
SET order_status = 1
WHERE order_status IS NULL;

SELECT * FROM orders;

/* - Begin a transaction when cancelling an order.
- Restore stock quantities based on order_items.
- Update order_status to 3.
- Use SAVEPOINT before stock restoration.
- If stock restoration fails, rollback to SAVEPOINT.
- Commit transaction only if all operations succeed. */

BEGIN TRY
BEGIN TRANSACTION;

-- Step 1: Update order status to Rejected (3)
UPDATE orders
SET order_status = 3
WHERE order_id = 201;

-- Step 2: Savepoint
SAVE TRANSACTION RestorePoint;

-- Step 3: Restore Stock
UPDATE p
SET p.stock_qty = p.stock_qty + oi.quantity
FROM products p
JOIN order_items oi 
ON p.product_id = oi.product_id
WHERE oi.order_id = 201;

-- Step 4: Validation
IF EXISTS (
    SELECT * FROM products WHERE stock_qty < 0
)
BEGIN
    RAISERROR('Stock restoration failed',16,1);
END

COMMIT TRANSACTION;

END TRY

BEGIN CATCH

IF @@TRANCOUNT > 0
ROLLBACK TRANSACTION RestorePoint;

PRINT 'Error occurred during order cancellation';

END CATCH;

--Check Order Status
SELECT order_id, order_status
FROM orders
WHERE order_id = 201;

--Check Restored Stock
SELECT * FROM products;
INSERT INTO order_items (order_item_id, order_id, product_id, quantity)
VALUES
(10,201,1,2),
(11,201,2,4);
--Check Order Items
SELECT * 
FROM order_items
WHERE order_id = 201;

--Verify Insert
SELECT * FROM orders WHERE order_id = 201;
SELECT * FROM order_items WHERE order_id = 201;

--Check Final Result
SELECT order_id, order_status FROM orders WHERE order_id = 201;
SELECT * FROM products;

