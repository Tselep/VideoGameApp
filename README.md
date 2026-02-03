🎮  TseleStation Store

Final project for Coding Factory 8.

A Video Game Store & Management System built with ASP.NET Core Razor Pages, featuring full CRUD operations, authentication/authorization, cart & orders, and a modern Bootstrap-based UI.

⸻

📌 Project Overview

The application allows users to browse video games, search through the catalog, add items to a cart, and place orders.
Administrators can manage the entire catalog (Games, Genres, Studios) through protected pages.

The project focuses on:
	•	Clean architecture
	•	Proper use of ASP.NET Core Identity
	•	Entity Framework Core with SQLite
	•	Usable and consistent UI

⸻

🧱 Technologies
	•	ASP.NET Core (.NET 8)
	•	Razor Pages (SSR)
	•	Entity Framework Core
	•	SQLite
	•	ASP.NET Core Identity
	•	Bootstrap 5
	•	Bootstrap Icons

⸻

📐 Architecture

The application follows a layered architecture:
	•	Domain Models
	•	Game
	•	Genre
	•	Studio
	•	Order
	•	OrderItem
	•	Data Access Layer
	•	ApplicationDbContext (EF Core)
	•	Migrations (Code First)
	•	Infrastructure / Services
	•	Cart handling
	•	Order logic
	•	Presentation Layer
	•	Razor Pages
	•	Shared Layout & Partial Views

⸻

🔐 Authentication & Authorization
	•	ASP.NET Core Identity
	•	User Registration / Login / Logout
	•	Role-based access control:
	•	Admin: Full CRUD access
	•	User: Browse catalog, cart, orders

Admin-only actions are protected at page level.

⸻

🎮 Functional Features

Games
	•	List with pagination
	•	Search by Title / Genre / Studio
	•	Details page
	•	Admin CRUD operations

Genres
	•	List with pagination
	•	Search by name
	•	Admin CRUD operations

Studios
	•	List with pagination
	•	Search by name or country
	•	Admin CRUD operations

Cart
	•	Session-based cart
	•	Add / Remove games
	•	Update quantities

Orders
	•	Checkout process
	•	Order creation with line items
	•	Order history per user
	•	Order details view

⸻

🔍 Search

Search functionality is implemented consistently across:
	•	Games
	•	Genres
	•	Studios

Search is preserved during pagination.

⸻

🎨 UI / UX
	•	Bootstrap 5 responsive layout
	•	Consistent navbar and footer
	•	Modern Home page with:
	•	Hero section
	•	Statistics cards
	•	Recently added games
	•	Hover effects and subtle animations

⸻

🗄 Database
	•	SQLite database
	•	EF Core Code-First approach
	•	Relationships:
	•	Game → Genre (Many-to-One)
	•	Game → Studio (Many-to-One)
	•	Order → OrderItems (One-to-Many)

⸻

▶️ How to Run

# Clone repository
git clone https://github.com/Tselep/VideoGameApp.git

# Navigate to project
cd VideoGameApp

# Apply migrations and run
dotnet ef database update
dotnet run

Open browser at:

https://localhost:5001


⸻

🔑 Admin Test Account

Email: admin@admin.com

Password: Admin123!


⸻

✅ Status

The project is complete and ready for evaluation.

⸻

👨‍💻 Author

Tselepis Spiros

⸻

✅ Project is complete and ready for evaluation.
