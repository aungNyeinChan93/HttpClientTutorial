# EF Core Scaffolding (Database First)

## What is Scaffolding?

Scaffolding is the process of generating Entity Models and DbContext from an existing database.

Database First Flow:

```text
SQL Server Database
        ↓
EF Core Scaffolding
        ↓
DbContext + Models
```

---

## Required Packages

Install EF Core packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Design
```

Install EF CLI Tool:

```bash
dotnet tool install --global dotnet-ef
```

Verify:

```bash
dotnet ef --version
```

---

## Basic Scaffold Command

### Windows Authentication

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

### SQL Authentication (sa)

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;User Id=sa;Password=YourPassword;TrustServerCertificate=True;Encrypt=False" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

---

## Generated Files

```text
Project
│
├── Models
│   ├── Game.cs
│   ├── Customer.cs
│   └── Order.cs
│
└── GameStoreDbContext.cs
```

---

## Scaffold Specific Tables

Generate only selected tables:

```powershell
dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
--table Games `
--table Customers `
-o Models
```

---

## Common Options

| Option            | Description              |
| ----------------- | ------------------------ |
| -o                | Output directory         |
| -c                | DbContext name           |
| -f                | Force overwrite          |
| --table           | Scaffold specific table  |
| --schema          | Scaffold specific schema |
| --project         | Target project           |
| --startup-project | Startup project          |

---

## Multi-Project Solution

Example Structure:

```text
GameStore.Api
GameStore.Database
```

Scaffold into Class Library:

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True" `
Microsoft.EntityFrameworkCore.SqlServer `
--project GameStore.Database `
--startup-project GameStore.Api `
-o Models `
-c GameStoreDbContext `
-f
```

Generated:

```text
GameStore.Database
│
├── Models
│   ├── Game.cs
│   └── Customer.cs
│
└── GameStoreDbContext.cs
```

---

## Register DbContext

Program.cs

```csharp
builder.Services.AddDbContext<GameStoreDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"));
});
```

---

## Common Errors

### Login Failed

```text
Login failed for user 'sa'
```

Check:

* SQL Server Authentication enabled
* Correct username/password
* sa account enabled

---

### EF Tool Not Found

```text
Could not execute because the specified command or file was not found.
```

Fix:

```bash
dotnet tool install --global dotnet-ef
```

---

### Missing Design Package

```text
Your startup project doesn't reference Microsoft.EntityFrameworkCore.Design
```

Fix:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

### Target Project Doesn't Match Migrations Assembly

```text
Your target project 'Api'
doesn't match your migrations assembly 'Database'
```

Fix:

```powershell
--project GameStore.Database
--startup-project GameStore.Api
```

or

```csharp
options.UseSqlServer(
    connectionString,
    b => b.MigrationsAssembly("GameStore.Database"));
```

---

## Useful Commands

### Regenerate Models

```powershell
dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

### List Installed Packages

```bash
dotnet list package
```

### Check EF Version

```bash
dotnet ef --version
```

---

## Notes

* `-f` overwrites existing files.
* Scaffolding is used for Database First development.
* Re-run scaffolding whenever database schema changes.
* Avoid manually editing generated files unless using partial classes.


## 
# EF Core Scaffolding (Database First)

## What is Scaffolding?

Scaffolding is the process of generating Entity Models and DbContext from an existing database.

Database First Flow:

```text
SQL Server Database
        ↓
EF Core Scaffolding
        ↓
DbContext + Models
```

---

## Required Packages

Install EF Core packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Design
```

Install EF CLI Tool:

```bash
dotnet tool install --global dotnet-ef
```

Verify:

```bash
dotnet ef --version
```

---

## Basic Scaffold Command

### Windows Authentication

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

### SQL Authentication (sa)

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;User Id=sa;Password=YourPassword;TrustServerCertificate=True;Encrypt=False" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

---

## Generated Files

```text
Project
│
├── Models
│   ├── Game.cs
│   ├── Customer.cs
│   └── Order.cs
│
└── GameStoreDbContext.cs
```

---

## Scaffold Specific Tables

Generate only selected tables:

```powershell
dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
--table Games `
--table Customers `
-o Models
```

---

## Common Options

| Option            | Description              |
| ----------------- | ------------------------ |
| -o                | Output directory         |
| -c                | DbContext name           |
| -f                | Force overwrite          |
| --table           | Scaffold specific table  |
| --schema          | Scaffold specific schema |
| --project         | Target project           |
| --startup-project | Startup project          |

---

## Multi-Project Solution

Example Structure:

```text
GameStore.Api
GameStore.Database
```

Scaffold into Class Library:

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True" `
Microsoft.EntityFrameworkCore.SqlServer `
--project GameStore.Database `
--startup-project GameStore.Api `
-o Models `
-c GameStoreDbContext `
-f
```

Generated:

```text
GameStore.Database
│
├── Models
│   ├── Game.cs
│   └── Customer.cs
│
└── GameStoreDbContext.cs
```

---

## Register DbContext

Program.cs

```csharp
builder.Services.AddDbContext<GameStoreDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"));
});
```

---

## Common Errors

### Login Failed

```text
Login failed for user 'sa'
```

Check:

* SQL Server Authentication enabled
* Correct username/password
* sa account enabled

---

### EF Tool Not Found

```text
Could not execute because the specified command or file was not found.
```

Fix:

```bash
dotnet tool install --global dotnet-ef
```

---

### Missing Design Package

```text
Your startup project doesn't reference Microsoft.EntityFrameworkCore.Design
```

Fix:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

### Target Project Doesn't Match Migrations Assembly

```text
Your target project 'Api'
doesn't match your migrations assembly 'Database'
```

Fix:

```powershell
--project GameStore.Database
--startup-project GameStore.Api
```

or

```csharp
options.UseSqlServer(
    connectionString,
    b => b.MigrationsAssembly("GameStore.Database"));
```

---

## Useful Commands

### Regenerate Models

```powershell
dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

### List Installed Packages

```bash
dotnet list package
```

### Check EF Version

```bash
dotnet ef --version
```

---

## Notes

* `-f` overwrites existing files.
* Scaffolding is used for Database First development.
* Re-run scaffolding whenever database schema changes.
* Avoid manually editing generated files unless using partial classes.
