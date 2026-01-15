# 🎮 VideoGameApp

Final project for **Coding Factory 8**.  
This application is a **Video Game Management System** built with ASP.NET Core Razor Pages, Entity Framework Core, and SQLite.

It implements full **CRUD functionality**, **Authentication / Authorization**, **Role-based access control**, and basic business rules.

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

- **Domain Models** (Game, Genre, Studio)
- **Data Access Layer** (EF Core / DbContext)
- **Infrastructure / Services**
- **Presentation Layer** (Razor Pages)

The database is created using a **Model-First approach** via EF Core migrations.

---

## 📊 Domain Model
The application manages:
- **Games**
- **Genres**
- **Studios**

### Relationships:
- Each Game belongs to **one Genre**
- Each Game belongs to **one Studio**

### Business Rules:
- A Genre or Studio cannot be deleted if it is used by any Game
- Appropriate validation messages are displayed in the UI

---

## 🔐 Authentication & Authorization

The application uses **ASP.NET Core Identity** for authentication and role-based authorization.

### Authentication
Supported features:
- User Registration
- User Login / Logout
- Cookie-based authentication
- Secure password hashing (ASP.NET Core Identity)

Identity UI endpoints:
- `/Identity/Account/Register`
- `/Identity/Account/Login`
- `/Identity/Account/Logout`

Navigation links (Login / Register / Logout) are displayed dynamically in the navbar based on the user’s authentication state.

---

### Authorization (Roles)

The application defines two roles:
- `Admin`
- `User`

#### Default Admin User
At application startup, a default admin user is automatically seeded:
- **Email:** `admin@admin.com`
- **Password:** `Admin123!`
- **Role:** `Admin`

---

### Role-based Access Control

| Action | User | Admin |
|------|------|-------|
| View lists & details | ✅ | ✅ |
| Create entities | ❌ | ✅ |
| Edit entities | ❌ | ✅ |
| Delete entities | ❌ | ✅ |

Access control is enforced:
- **Backend:** `[Authorize(Roles = "Admin")]` on Create/Edit/Delete Razor Pages
- **Frontend:** Create/Edit/Delete buttons are hidden for non-admin users

Even if a non-authorized user attempts to access protected URLs directly, they are redirected to **Access Denied**.

---

## 🗄️ Database
The application uses **SQLite** (single-file database).

Database file:
- `videogameapp.db`

The database includes:
- Domain tables (Games, Genres, Studios)
- Identity tables:
  - `AspNetUsers`
  - `AspNetRoles`
  - `AspNetUserRoles`
  - `AspNetUserClaims`
  - and others

EF Core migrations are applied automatically at startup.

---

## ▶️ Build & Run

### Prerequisites
- .NET SDK 8.0+

### Run the application
```bash
dotnet restore
dotnet run
