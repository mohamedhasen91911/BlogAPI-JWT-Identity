# Blog API (ASP.NET Core + JWT + Identity)

## Features
- Register / Login with JWT
- Roles (Admin, User)
- CRUD for Blog Posts
- Role-based Authorization

## Technologies
- ASP.NET Core 8 Web API
- Entity Framework Core + SQLite/SQL Server
- Identity
- JWT Authentication
- Repository + Service Pattern
- AutoMapper

## How to Run
1. Clone the repo:
   ```bash
   git clone https://github.com/mohamedhasen91911/BlogAPI-JWT-Identity.git
   ```
2.	Update appsettings.json with your database and JWT settings.
3.	Run migrations:
    ```bash
    dotnet ef database update
    ```
4.	Run the project:
    ```bash
    dotnet run
    ```

## Endpoints
	•	POST /api/auth/register
	•	POST /api/auth/login
	•	GET /api/posts (User/Admin)
	•	POST /api/posts (Admin)
	•	PUT /api/posts/{id} (Admin)
	•	DELETE /api/posts/{id} (Admin)

    
