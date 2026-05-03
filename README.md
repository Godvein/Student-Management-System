# Student Management System API (ASP.NET Core Web API)

This is a backend **learning project** built using **ASP.NET Core Web API** and **Entity Framework Core**.  
It demonstrates a complete backend system with authentication, authorization, and CRUD operations.

The goal of this project was to understand how real-world backend systems are structured and how authentication/authorization works using JWT tokens.

---

## 🚀 Features

- User Authentication (Register & Login)
- JWT Token-based Authentication
- Role-based Authorization (Admin / User)
- CRUD operations for:
  - Students
  - Teachers
- Many-to-Many relationship between Users and Roles
- Password hashing using BCrypt
- Entity Framework Core with SQL Server / LocalDB
- Seed data for initial admin and roles

---

## 🧱 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (LocalDB)
- JWT Authentication
- BCrypt for password hashing
- C#

---

## 🔐 Authentication Flow

1. User registers or logs in
2. Server validates credentials
3. JWT token is generated
4. Token is sent to client
5. Client uses token in `Authorization: Bearer <token>` header
6. API validates token and authorizes based on roles

---

## 🧑‍💻 Role-based Authorization

The system supports role-based access control:

- **Admin**
  - Can manage users
  - Can manage teachers and students
  - Can assign roles

- **User**
  - Limited access to API endpoints


## API Endpoints
**Authentication**
- POST /api/Auth/register  – (Register new user)
- POST /api/Auth/login – (Login and receive JWT token)

**Students**
- GET /api/Student – Get all students
- POST /api/Student – Add student (Admin only)
- PUT /api/Student/{id} – Update student (Admin only)
- DELETE /api/Student/{id} – Delete student (Admin only)

**Teachers**
- GET /api/Teacher – Get all teachers
- POST /api/Teacher – Add teacher (Admin only)
- PUT /api/Teacher/{id} – Update student (Admin only)
- DELETE /api/Teacher/{id} – Delete student (Admin only)

**Admin**
- POST /api/Admin/assign-role?roleId=n&userId=n (Admin only)
- POST /api/Admin/remove-role?roleId=n&userId=n (Admin only)

## 🛠️ How to Run the Project

### 1. Clone the repository

```bash
git clone https://github.com/Godvein/Student-Management-System.git
cd Student-Management-System
```

### 2. Install required tools

Make sure you have the following installed:

- .NET SDK
- .NET 10
- mysql Database
- SQL Server (LocalDB is used in this project)
- SQL Server Management Studio (optional but recommended)

### 3. Install required NuGet packages

Run these commands if packages are not restored automatically:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package BCrypt.Net-Next
```

### 4. Restore dependencies
```bash
dotnet restore
```
###5. Update database connection string

### Update appsettings.json:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;"
}
```
### 6. Run migrations
```bash
dotnet ef database update
```
If EF tools are missing:
```bash
dotnet tool install --global dotnet-ef
```
### 7. Run the project
```bash
dotnet run
```

### 8. Test API

Use tools like:

Postman
Swagger UI (/swagger)
