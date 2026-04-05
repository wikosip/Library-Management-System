# Library Management System

A comprehensive backend solution for managing library operations, built using modern .NET technologies and architectural patterns.

Tech Stack:
* **Language:** C#
* **Framework:** .NET 8
* **Database:** SQL Server
* **ORM:** Dapper (High-performance micro-ORM)
* **Architecture:** Repository Pattern & Unit of Work

Key Features:
* **Author Management:** Full CRUD operations and advanced search.
* **Book Inventory:** Organized by genres, languages, and publishers.
* **Transaction Management:** Implemented via the **Unit of Work** pattern to ensure data consistency (ACID principles).
* **Automated Testing:** Unit tests for repositories using NUnit.

Architectural Overview:
The project follows a **Layered Architecture** approach, separating business logic from data access. This structure enhances testability and maintainability. It specifically addresses complex SQL transactions and connection lifecycle management in a multi-repository environment.

How to Run:
1. Clone the repository.
2. Update the connection string in `appsettings.json`.
3. Rebuild Solution.
4. Run `Update-Database` in Package Manager Console.
5. Start the application.
