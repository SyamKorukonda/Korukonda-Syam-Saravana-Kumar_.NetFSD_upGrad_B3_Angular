
--The sales team wants a product listing categorized by product category 
--along with brand details to understand product distribution.

USE HandsOn;

CREATE TABLE Brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50)
);

CREATE TABLE Categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);

 CREATE TABLE Products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),
    FOREIGN KEY (brand_id) REFERENCES Brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES Categories(category_id)
);

INSERT INTO Brands VALUES
(1, 'Nike'),
(2, 'Adidas'),
(3, 'Puma');

INSERT INTO Categories VALUES
(1, 'Shoes'),
(2, 'Clothing'),
(3, 'Accessories');

INSERT INTO Products VALUES
(101, 'Air Zoom', 1, 1, 2023, 800),
(102, 'Ultraboost', 2, 1, 2022, 900),
(103, 'Running Tee', 3, 2, 2023, 300),
(104, 'Sports Cap', 1, 3, 2021, 200),
(105, 'Track Jacket', 2, 2, 2024, 650);

SELECT * FROM Brands;
SELECT * FROM Products;
SELECT * FROM Categories;

--1. Display product_name, brand_name, category_name, model_year, and list_price.
--2. Filter products with list_price greater than 500.
--3. Sort results by list_price in ascending order.

SELECT p.product_name,b.brand_name,c.category_name,p.model_year,p.list_price
FROM Products p
JOIN Brands b ON p.brand_id=b.brand_id
JOIN Categories c ON c.category_id=p.category_id
where p.list_price > 500
ORDER BY p.list_price;


