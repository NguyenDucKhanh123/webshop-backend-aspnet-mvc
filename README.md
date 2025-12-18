# WebShopApp – ASP.NET Core E-Commerce Web Application

A full-stack web application for managing an online shop, built with ASP.NET Core MVC and Entity Framework.  
The system supports product management, user authentication, shopping cart, order processing, and admin control.

---

## Table of Contents
- Introduction
- Technologies
- Features
- Project Structure
- Installation
- Usage
- Notes
- Contact

---

## Introduction

WebShopApp is an e-commerce web application designed to allow users to browse products, add items to cart, and place orders.  
The system is built with ASP.NET Core and follows the MVC architecture. It includes role-based access for customers and administrators.

---

## Technologies

### Backend & Frontend
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap

### Others
- Git & GitHub
- LINQ
- JSON
- Role-based Authorization

---

## Features

### System Features
- User registration & login
- Product listing & detail view
- Shopping cart functionality
- Order placement & history
- Admin dashboard for product & user management
- Responsive UI with Razor Views
- Data seeding for demo/testing

---

## Project Structure

```
WebShopApp/
│
├── .vs/                      # Visual Studio settings
├── Areas/                    # Admin area
├── bin/                      # Build output
├── Controllers/              # MVC controllers
├── Data/                     # Database context
├── Migrations/               # EF Core migrations
├── Models/                   # Data models
├── obj/                      # Temporary build files
├── Properties/               # Project settings
├── Service/                  # Business logic services
├── ViewModel/                # View models
├── Views/                    # Razor views
├── wwwroot/                  # Static files (CSS, JS, images)
│
├── appsettings.json          # Main configuration
├── appsettings.Development.json
├── Program.cs                # Application entry point
├── ScaffoldingReadMe.txt     # Notes from scaffolding
├── WebShopApp.csproj         # Project file
├── WebShopApp.csproj.user    # User-specific settings
└── WebShopApp.sln            # Solution file
```

---

## Installation

### Prerequisites
- .NET SDK (v7.0+ recommended)
- SQL Server
- Visual Studio or VS Code

---

### Backend Setup

1. Navigate to the project folder:
```bash
cd WebShopApp
```

2. Restore and build the project:
```bash
dotnet restore
dotnet build
```

3. Update database (if using EF Core migrations):
```bash
dotnet ef database update
```

4. (Optional) Seed sample data:
```bash
dotnet run -- seed
```

5. Run the application:
```bash
dotnet run
```

Application will run at:  
`https://localhost:5001` or `http://localhost:5000`

---

## Usage

- Register or login as a customer to browse and shop products.  
- Add items to cart and place orders.  
- Admin users can manage products, categories, and users via the dashboard.  
- View order history and manage account settings.

---

## Notes

- Ensure SQL Server is running and accessible before starting the backend.  
- Default backend port: `5000` (check `Program.cs` for custom settings).  
- Update environment variables as needed for production deployment.  
- For demo/testing, sample data is available in `Data/SeedData.cs`.  
- If you encounter CORS or HTTPS issues, check `launchSettings.json` and `appsettings.json`.

---

## Contact

For questions or collaboration, please contact:

Nguyen Duc Khanh  
GitHub: [NguyenDucKhanh123](https://github.com/NguyenDucKhanh123)  
Email: khanh.nd11246@sinhvien.hoasen.edu.vn
