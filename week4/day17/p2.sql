/*Problem 2: Atomic Order Cancellation with SAVEPOINT
Scenario
When cancelling an order, system must restore stock quantities and update order_status to Rejected (3). All actions must be atomic  */
USE Hand_17;


INSERT INTO orders (order_id, order_date, order_status)
VALUES (201, GETDATE(), 1);

SELECT * FROM orders;


SELECT * FROM order_items;

--Check Current Stock

SELECT * FROM products;

/* - Begin a transaction when cancelling an order.
- Restore stock quantities based on order_items.
- Update order_status to 3.
- Use SAVEPOINT before stock restoration.
- If stock restoration fails, rollback to SAVEPOINT.
- Commit transaction only if all operations succeed. */

BEGIN TRY
BEGIN TRANSACTION;   --A transaction ensures that all related operations happen together or none happen
 
-- Step 1: Update order status to Rejected (3)

UPDATE orders
SET order_status = 3    --Mark order as Rejected.
WHERE order_id = 201;  

-- Step 2: Savepoint

SAVE TRANSACTION RestorePoint;  --if stock restoration fails, we rollback only to this point

-- Step 3: Restore Stock

UPDATE p                      --Add back the quantities that were reduced when the order was placed.
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

COMMIT TRANSACTION;               --save all changes permanently.

Workflow:

END TRY

BEGIN CATCH

IF @@TRANCOUNT > 0  -- it is a system variable ,it Ensure a transaction is active before rollback
ROLLBACK TRANSACTION RestorePoint;

PRINT 'Error occurred during order cancellation';

END CATCH;

--Check Order Status
SELECT order_id, order_status
FROM orders
WHERE order_id = 201;

--Check Restored Stock
SELECT * FROM products;

--Verify Insert
SELECT * FROM orders WHERE order_id = 201;

--Check Final Result
SELECT order_id, order_status FROM orders WHERE order_id = 201;
SELECT * FROM products;

