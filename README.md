# 🎮 VideoGameApp

A web application for managing video games, genres and studios, built with ASP.NET Core Razor Pages, Entity Framework Core and SQLite.

The application demonstrates a complete domain model with relationships, CRUD functionality and basic business rules.

---

## 🧱 Domain Model

The application is based on the following entities:

- **Game**
- **Genre**
- **Studio**

### Relationships
- Each **Game** belongs to one **Genre**
- Each **Game** belongs to one **Studio**

Entity relationships are implemented using **Foreign Keys** and **Navigation Properties** with Entity Framework Core.

---

## 🗄️ Database

- **SQLite** database
- **Entity Framework Core (Code First)**
- Migrations are applied automatically on startup
- The database is created automatically if it does not exist
- Initial seed data is inserted

---

## ⚙️ Functionality

### CRUD Operations
The application supports full CRUD functionality for:

- Games
- Genres
- Studios

### Business Rules
- A **Genre** or **Studio** cannot be deleted if it is referenced by existing Games
- Friendly warning messages are shown instead of database errors
- Delete actions are disabled in the UI when an entity is in use

---

## 🏠 Home Page (Dashboard)

The home page provides an overview of the application:

- Total number of Games
- Total number of Genres
- Total number of Studios
- List of recently added Games
- Quick navigation buttons

---

## 🖥️ Technologies Used

- ASP.NET Core Razor Pages
- Entity Framework Core
- SQLite
- Bootstrap 5
- C#

---

## ▶️ Build & Run (Local)

### Prerequisites
- .NET SDK 7.0 or newer

### Steps

```bash
git clone https://github.com/Tselep/VideoGameApp.git
cd VideoGameApp
dotnet restore
dotnet run
