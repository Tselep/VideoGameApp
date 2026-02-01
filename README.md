# 🎮 VideoGameApp  
Final Project – Coding Factory 8

A Video Game Management & E-Shop System built with ASP.NET Core Razor Pages, Entity Framework Core, and SQLite.

The application demonstrates a complete web application workflow, including CRUD operations, Authentication & Authorization, role-based access control, shopping cart, orders, pagination, and database seeding.

---

## 🧱 Technologies
- ASP.NET Core (.NET 8)
- Razor Pages (Server-Side Rendering)
- Entity Framework Core
- SQLite
- ASP.NET Core Identity
- Bootstrap 5

---

## 📐 Architecture
The application follows a layered architecture:

- Domain Models (Game, Genre, Studio, Order, OrderItem)
- Data Access Layer (Entity Framework Core / DbContext)
- Infrastructure & Services
- Presentation Layer (Razor Pages)

The database is created using a Model-First approach through EF Core Migrations.

---

## 📊 Domain Model

### Entities
- Games
- Genres
- Studios
- Orders
- Order Items

### Relationships
- Each Game belongs to one Genre
- Each Game belongs to one Studio
- Each Order belongs to one User
- Each Order contains multiple Games

### Business Rules
- A Genre or Studio cannot be deleted if it is used by any Game
- Proper validation messages are displayed in the UI
- Prices and totals are calculated automatically (Cart & Orders)

---

## 🔐 Authentication & Authorization
The application uses ASP.NET Core Identity.

### Authentication
Supported features:
- User Registration
- User Login / Logout
- Cookie-based authentication
- Secure password hashing (ASP.NET Core Identity)

Identity endpoints:
- /Identity/Account/Register
- /Identity/Account/Login
- /Identity/Account/Logout

Navigation links (Login / Register / Logout / Username) are rendered dynamically based on authentication state.

---

## 👥 Authorization (Roles)
The application defines two roles:

- Admin
- User

### Default Admin User
At application startup, a default admin account is seeded automatically:

- Email: admin@admin.com
- Password: Admin123!
- Role: Admin

### Role-Based Access Control

Action | User | Admin
------ | ---- | -----
View lists & details | ✅ | ✅
Create entities | ❌ | ✅
Edit entities | ❌ | ✅
Delete entities | ❌ | ✅
Add to cart / Place orders | ✅ | ✅

Access control is enforced:
- Backend: [Authorize(Roles = "Admin")] on Create/Edit/Delete pages
- Frontend: Admin buttons are hidden for non-admin users
- Unauthorized access redirects to Access Denied

---

## 🛒 Shopping Cart & Orders

### Shopping Cart
- Add games to cart
- Increase / decrease quantity
- Automatic total calculation
- Cart indicator in the navigation bar

### Checkout & Orders
- Checkout page with order summary
- Order creation
- Order history per user
- Order details page

---

## 📄 Pagination
Pagination is implemented for:
- Games
- Genres
- Studios

This ensures better performance, clean UI, and realistic application behavior with large datasets.

---

## 🌱 Database Seeding
The database is automatically seeded at application startup.

Seeded data includes:
- 10 Genres
- 10 Studios (with Country information)
- 20+ Games
- Default Admin User & Roles

Seeding logic is idempotent:
- No duplicate data is created
- Safe to run multiple times
- Missing fields are backfilled automatically

This allows the project to be cloned and run immediately without manual configuration.

---

## 🗄️ Database
The application uses SQLite (single-file database).

Database file:
videogameapp.db

The database includes:
- Domain tables (Games, Genres, Studios, Orders)
- Identity tables:
  - AspNetUsers
  - AspNetRoles
  - AspNetUserRoles
  - AspNetUserClaims
  - and others

EF Core migrations are applied automatically.

---

## ▶️ Build & Run

### Prerequisites
- .NET SDK 8.0+

### Run the application
dotnet restore  
dotnet run  

On startup, the application will:
- Create the database
- Apply migrations
- Seed initial data automatically

---

## ✅ Final Delivery Checklist
- [x] ASP.NET Core Razor Pages
- [x] Entity Framework Core with SQLite
- [x] Full CRUD functionality
- [x] Authentication & Authorization
- [x] Role-based access control
- [x] Shopping cart & orders
- [x] Pagination
- [x] Database seeding
- [x] Clean Bootstrap UI
- [x] Ready-to-run project

---

## 🏁 Conclusion
This project demonstrates a complete and realistic ASP.NET Core web application, combining administrative management with an e-commerce workflow.

It is designed to be easy to run, easy to review, and easy to extend.
