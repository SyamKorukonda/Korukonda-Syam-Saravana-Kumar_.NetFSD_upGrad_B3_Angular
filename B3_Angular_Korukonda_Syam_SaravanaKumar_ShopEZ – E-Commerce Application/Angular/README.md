# ShopEZ –E-Commerce Application

## Project Overview

ShopEZ is a Full Stack E-Commerce Web Application developed using Angular, ASP.NET Core Web API, Entity Framework Core, and SQL Server.

The application provides a modern online shopping experience where customers can browse products, manage their shopping cart, and place orders, while administrators can manage products and monitor orders.

This project demonstrates:
- Full Stack Development
- REST API Integration
- Angular Single Page Application (SPA)
- Database Management using SQL Server
- Authentication and Authorization
- Responsive UI Design

---

# Problem Statement

Small businesses often struggle to create affordable online shopping platforms. Many rely on manual systems or static websites that lack:

- Product management
- Shopping cart functionality
- Order processing
- Dynamic user experience

Customers expect modern applications that allow them to:

- Browse products easily
- Add products to cart
- Place orders online
- Access product information quickly

ShopEZ solves these challenges by providing a complete e-commerce platform using modern web technologies.

---

# Project Objectives

The main objectives of this project are:

- Build a responsive product catalog website
- Implement shopping cart functionality
- Develop REST APIs for product and order management
- Store data using SQL Server
- Build a Single Page Application (SPA) using Angular
- Implement authentication and authorization
- Demonstrate full-stack architecture concepts

---

# System Architecture

## High Level Architecture

- Angular Frontend (SPA)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Database
- API Gateway (Ocelot)
- JWT Authentication

---

# Technologies Used

## Frontend
- Angular
- TypeScript
- HTML5
- CSS3
- Bootstrap
- Angular Router
- Angular Reactive Forms
- HttpClient

## Backend
- ASP.NET Core Web API
- C#
- Entity Framework Core
- LINQ
- JWT Authentication

## Database
- SQL Server

## API Gateway
- Ocelot API Gateway

## Testing Tools
- Karma
- Jasmine
- Swagger
- Postman

---

# Functional Requirements

## Customer Features

- View product list
- View product details
- Search products
- Filter products by category
- Add products to cart
- Remove products from cart
- Update cart quantity
- Place orders
- View order history
- User registration
- User login/logout

## Admin Features

- Add products
- Edit products
- Delete products
- View all orders
- Manage users
- Dashboard analytics

---

# Non-Functional Requirements

| Requirement | Description |
|---|---|
| Performance | Application should load products quickly |
| Security | API endpoints should validate data |
| Scalability | Modular architecture |
| Usability | Responsive user interface |

---

# Angular Application Structure

```plaintext
src/app
│
├── components
│   ├── navbar
│   ├── footer
│   ├── cart
│
├── pages
│   ├── home
│   ├── product-list
│   ├── product-details
│   ├── checkout
│   ├── my-orders
│   ├── login
│   ├── register
│   └── admin
│       ├── admin-dashboard
│       ├── admin-products
│       ├── admin-orders
│       └── admin-users
│
├── services
│   ├── auth.service.ts
│   ├── product.service.ts
│   ├── cart.service.ts
│   └── order.service.ts
│
├── models
│   ├── product.model.ts
│   ├── order.model.ts
│   ├── user.model.ts
│   └── cart-item.model.ts
│
├── guards
│   ├── auth.guard.ts
│   └── admin.guard.ts
│
└── app.routes.ts