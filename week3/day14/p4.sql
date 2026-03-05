USE Day14;

-- Create Orders Table
CREATE TABLE Orders1 (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_date DATE,
    required_date DATE,
    shipped_date DATE,
    order_status INT
);

-- Create Archive Table
CREATE TABLE archived_orders (
    order_id INT,
    customer_id INT,
    order_date DATE,
    required_date DATE,
    shipped_date DATE,
    order_status INT
);

-- Insert Sample Data
-- order_status: 3 = Rejected, 4 = Completed
INSERT INTO Orders1 VALUES
(1,101,'2025-01-10','2025-01-15','2025-01-14',4),
(2,102,'2025-02-01','2025-02-05','2025-02-07',4),
(3,103,'2023-01-05','2023-01-10','2023-01-09',3),
(4,101,'2024-06-10','2024-06-15','2024-06-14',4),
(5,104,'2022-12-01','2022-12-05','2022-12-04',3);

-- 1️⃣ Insert archived records (Rejected and older than 1 year)
INSERT INTO archived_orders
SELECT *
FROM Orders1
WHERE order_status = 3
AND order_date < DATEADD(YEAR,-1,GETDATE());

-- 2️ Delete rejected orders older than 1 year
DELETE FROM Orders1
WHERE order_status = 3
AND order_date < DATEADD(YEAR,-1,GETDATE());

-- 3️ Identify customers whose all orders are completed
-- 4️ Display processing delay
-- 5️ Mark orders as Delayed / On Time
SELECT 
o.order_id,
o.customer_id,
DATEDIFF(DAY,o.order_date,o.shipped_date) AS processing_delay,
CASE 
    WHEN o.shipped_date > o.required_date THEN 'Delayed'
    ELSE 'On Time'
END AS delivery_status
FROM Orders1 o
WHERE o.customer_id IN
(
    SELECT customer_id
    FROM Orders1
    GROUP BY customer_id
    HAVING COUNT(*) =
    SUM(CASE WHEN order_status = 4 THEN 1 ELSE 0 END)
)
AND o.order_status <> 3;