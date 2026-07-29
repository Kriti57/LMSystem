# Library Management System

A role-based Library Management System built using **ASP.NET Core MVC**, **Entity Framework Core**, and **SQL Server**. The application streamlines library operations by allowing administrators and librarians to manage books, magazines, newspapers, and user borrowing activities through a secure, user-friendly web interface.

## Features

### Authentication & Authorization
- ASP.NET Core Identity authentication
- Role-based access control
- Administrator, Librarian, and Member roles
- Secure login and registration

### Book Management
- Add, edit, delete, and view books
- Search books by title or author
- Filter books by availability
- Borrow and return books

### Magazine Management
- Add, edit, delete, and view magazines
- Search by title or publisher
- Track borrowing status

### Newspaper Management
- Add, edit, delete, and view newspapers
- Search and manage circulation
- Track borrowing status

### Member Features
- View available resources
- Borrow and return library items
- View personal borrowing history
- Manage issued items

### Dashboard
- Total library resources
- Currently borrowed items
- Registered members
- Library overview for administrators

## Tech Stack

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Razor Views
- HTML5
- CSS3
- Bootstrap
- JavaScript

## Project Structure

```
LMSystem/
├── Controllers/
├── Data/
├── Migrations/
├── Models/
├── Views/
├── wwwroot/
├── appsettings.json
├── Program.cs
├── LMSystem.csproj
└── README.md
```

## Installation

### Clone the repository

```bash
git clone https://github.com/Kriti57/LMSystem.git
```

### Navigate to the project

```bash
cd LMSystem
```

### Restore packages

```bash
dotnet restore
```

### Update the connection string

Modify the connection string inside:

```
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LibraryManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### Apply migrations

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

or launch the project using Visual Studio.

## User Roles

| Role | Permissions |
|------|-------------|
| Administrator | Full access to all resources and user management |
| Librarian | Manage library resources and borrowing activities |
| Member | Borrow and return resources, view personal borrow history |

## Future Enhancements

- Email notifications for due dates
- Fine calculation for overdue items
- Barcode or QR code support
- Advanced search and filtering
- Report generation
- Analytics dashboard
