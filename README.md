🎮 TseleStation Store

A Video Game Store & Management System built as a final project for Coding Factory 8.

The application allows users to browse video games, manage genres and studios, add items to a cart, and place orders, while administrators can fully manage the catalog.

⸻

🚀 Features

👤 Users
	•	Browse video games with search & pagination
	•	View game details
	•	Add games to cart
	•	Update cart quantities / remove items
	•	Checkout and place orders
	•	View order history and order details

🔐 Authentication & Authorization
	•	ASP.NET Core Identity
	•	Register / Login / Logout
	•	Role-based access:
	•	Admin: Full CRUD access
	•	User: Read-only catalog + orders

🛠 Admin Features
	•	Create / Edit / Delete Games
	•	Create / Edit / Delete Genres
	•	Create / Edit / Delete Studios
	•	Protected admin-only actions

🔍 Search
	•	Search implemented on:
	•	Games (by Title / Genre / Studio)
	•	Genres (by Name)
	•	Studios (by Name / Country)
	•	Search is preserved during pagination

🛒 Cart & Orders
	•	Session-based cart
	•	Order creation with line items
	•	Order totals calculation
	•	Order details view

⸻

🎨 UI / UX
	•	Built with Bootstrap 5
	•	Responsive layout
	•	Modern Hero section on Home page
	•	Animated stat cards & hover effects
	•	Consistent navbar & layout across pages

⸻

🧱 Architecture

The application follows a layered architecture:
	•	Presentation Layer: Razor Pages
	•	Infrastructure Layer: Services & Repositories
	•	Data Access Layer: Entity Framework Core
	•	Domain Models: Game, Genre, Studio, Order, OrderItem

⸻

🧪 Technologies
	•	ASP.NET Core (.NET 8)
	•	Razor Pages
	•	Entity Framework Core
	•	SQLite
	•	ASP.NET Core Identity
	•	Bootstrap 5
	•	Bootstrap Icons

⸻

🗄 Database
	•	SQLite database
	•	Code-First approach using EF Core Migrations
	•	Relationships:
	•	Game → Genre (Many-to-One)
	•	Game → Studio (Many-to-One)
	•	Order → OrderItems (One-to-Many)

⸻

▶️ How to Run the Project
	1.	Clone the repository:

git clone https://github.com/USERNAME/TseleStationStore.git

	2.	Navigate to the project folder:

cd VideoGameApp

	3.	Apply migrations & run:

dotnet ef database update
dotnet run

	4.	Open browser:

https://localhost:5001


⸻

🔑 Admin Account (for testing)

Email: admin@admin.com
Password: Admin123!

Credentials are seeded on first run (if enabled in the project).

⸻

📌 Notes
	•	The project was developed as part of Coding Factory 8 final assignment.
	•	Focus was given to clean architecture, readable code, and user experience.

⸻

👨‍💻 Author

Tselepis Spiros

⸻

✅ Project is complete and ready for evaluation.
