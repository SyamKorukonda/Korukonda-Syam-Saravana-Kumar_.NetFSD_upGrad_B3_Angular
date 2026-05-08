# 🛒 ShopEZ – E-Commerce Application

A scalable and secure E-Commerce Backend API built using ASP.NET Core Web API.
This project implements authentication, product management, and order processing using JWT-based security and role-based authorization.

## Features

- JWT Authentication & Authorization
- User Registration & Login
- Product Management (Admin only)
- Order Management System
- Admin User Management
- Global Exception Handling Middleware
- Swagger API Documentation

## Architecture 

    Client (Postman / Browser)
        ↓
    Controller Layer
        ↓
    Service Layer
        ↓
    Repository Layer
        ↓
    DbContext (EF Core)
        ↓
    SQL Server Database


## Architecture Overview

   This project follows a Layered Architecture:

- Controllers → Handle HTTP requests
- Services → Business logic
- Repositories → Data access layer
- Models → Database entities
- DTOs → Data transfer objects
- Middleware → Global exception handling

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt Password Hashing
- Swagger (OpenAPI)

## Project Structure
- Controllers/     → API endpoints  
- Services/        → Business logic  
- Repositories/    → Data access layer  
- Models/          → Database entities  
- DTOs/            → Data transfer objects  
- Middleware/      → Global exception handling  
- docs/            → Architecture diagram  

## Authentication

- This API uses JWT (JSON Web Token).

- How to Use Token

- Add token in request header:

- Authorization: Bearer <your_token>

## API Endpoints

   ## Authentication
    Method	    Endpoint	               Description
   
    POST	   /api/Auth/register	      Register new user
    POST	   /api/Auth/login	          Login & get JWT token

   ## Products

    Method	        Endpoint	                 Access
    
    GET	            /api/Products	             Public
    GET	            /api/Products/{id}	         Public
    POST	        /api/Products	             Admin
    PUT	            /api/Products/{id}	         Admin
    DELETE	        /api/Products/{id}	         Admin 

  ## Orders

    Method	        Endpoint	                Access
    
    POST	        /api/Orders	                User
    GET	            /api/Orders/my-orders	    User
    GET	            /api/Orders/all-orders	    Admin
    GET	            /api/Orders/{id}	        User / Admin  

  ## Users
    Method	        Endpoint	            Access
    
    GET	            /api/Users	            Admin


## API Response Format
 - Success
    {
    "success": true,
    "message": "Success",
    "data": {}
    }
- Error
    {
    "success": false,
    "message": "Error message",
    "data": null
    }

## Error Handling

   - Handled globally using middleware.

   - Status Code	Meaning
  
   - 400	        Bad Request
   - 401	        Unauthorized
   - 403	        Forbidden
   - 404	        Not Found
   - 500	        Internal Server Error

## Security Features

- Password hashing using BCrypt
- JWT-based authentication
- Role-based authorization (Admin / Customer)
- Protected API endpoints

## Business Logic Highlights

  -  Stock validation before placing order
  -  Stock deduction after order
  -  Users can access only their own orders
  -  Admin has full access


## Setup & Run

  ##  1.Clone Repository
  - git clone https://github.com/SyamKorukonda/shopez.git

  ## 2.Configure Database

  -  Update appsettings.json:

    "ConnectionStrings": {
    "DefaultConnection": "Your_SQL_Server_Connection"
    }

 ## 3.Run Migrations
  
  -  Add-Migration InitialCreate
  -  Update-Database

 ## 4.Run Application
   -   dotnet run

 ## 5️.Open Swagger
  -     https://localhost:<port>/swagger

## Request Flow
 -  
    Client → Middleware → Authentication → Authorization → Controller → Service → Repository → Database → Response

## Future Enhancements

 - Payment Integration
 - Order Status Tracking
 - Refresh Token Implementation
 - Pagination & Filtering


## Author

 KORUKONDA SYAM SARAVANA KUMAR 
 Email: syam261004@gmail.com 