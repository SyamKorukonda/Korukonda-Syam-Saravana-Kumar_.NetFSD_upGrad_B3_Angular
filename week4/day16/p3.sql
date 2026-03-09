
--Before updating order_status in orders table, ensure that shipped_date is not NULL when status is set to Completed (4).

USE SalesDB;

CREATE TABLE orders1 (
    order_id INT PRIMARY KEY,
    order_date DATE,
    shipped_date DATE,
    order_status INT
);

INSERT INTO orders1 VALUES
	(1,'2024-03-01',NULL,1),
	(2,'2024-03-02','2024-03-05',2),
	(3,'2024-03-03',NULL,2);

	-- Create an AFTER UPDATE trigger on orders.

	CREATE TRIGGER trg_CheckShippedDate
		ON orders1
		AFTER UPDATE
		AS
		BEGIN
			BEGIN TRY

				IF EXISTS (
					SELECT 1
					FROM inserted
					WHERE order_status = 4
					AND shipped_date IS NULL
				)
				BEGIN
					RAISERROR('Shipped date must not be NULL when order status is Completed.',16,1)
					ROLLBACK TRANSACTION
					RETURN
				END

			END TRY

			BEGIN CATCH
				THROW
			END CATCH
		END

-- Validate that shipped_date is NOT NULL when order_status = 4.
UPDATE orders1
SET order_status = 4
WHERE order_id = 1;

--Shipped date must not be NULL when order status is Completed.

-- Prevent update if condition fails.
UPDATE orders1
SET shipped_date = '2024-03-10', order_status = 4
WHERE order_id = 3;

--Update successful.