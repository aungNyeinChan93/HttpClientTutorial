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


##

# EF Core Scaffolding (Database First)

## Scaffolding ဆိုတာဘာလဲ?

Scaffolding ဆိုတာ ရှိပြီးသား Database ထဲက Tables, Views, Relationships တွေကို အခြေခံပြီး

* Entity Models
* DbContext

တွေကို Auto Generate လုပ်ပေးတဲ့ Process ဖြစ်ပါတယ်။

```text
SQL Server Database
        ↓
EF Core Scaffolding
        ↓
DbContext + Models
```

Database First Approach မှာ အသုံးများပါတယ်။

---

# လိုအပ်သော Packages များ

EF Core SQL Server Provider Install လုပ်ရန်

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Design
```

EF CLI Tool Install လုပ်ရန်

```bash
dotnet tool install --global dotnet-ef
```

Version စစ်ရန်

```bash
dotnet ef --version
```

---

# Basic Scaffold Command

## Windows Authentication အသုံးပြုခြင်း

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;Trusted_Connection=True;TrustServerCertificate=True" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

### Command အဓိပ္ပါယ်

| Option    | အဓိပ္ပါယ်                                 |
| --------- | ----------------------------------------- |
| -o Models | Models Folder ထုတ်ပေးမည်                  |
| -c        | DbContext Name သတ်မှတ်မည်                 |
| -f        | ရှိပြီးသား File များကို Overwrite လုပ်မည် |

---

## SQL Authentication (sa Account)

```powershell
dotnet ef dbcontext scaffold `
"Server=DESKTOP-8CJL57G\SQLEXPRESS;Database=GameStore;User Id=sa;Password=YourPassword;TrustServerCertificate=True;Encrypt=False" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

---

# Generate လုပ်ပြီးနောက် ရရှိမည့် Files

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

# Table တချို့ကိုသာ Generate လုပ်ခြင်း

Database Table အားလုံး မလိုအပ်ဘဲ တချို့ကိုသာ Generate လုပ်ချင်ရင်

```powershell
dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
--table Games `
--table Customers `
-o Models
```

Games နှင့် Customers Table များသာ Generate လုပ်မည်။

---

# Multi-Project Solution

ဥပမာ

```text
GameStore.Api
GameStore.Database
```

### GameStore.Database ထဲသို့ Generate လုပ်ခြင်း

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

ရလာမည့် Structure

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

# DbContext ကို Dependency Injection ထဲ Register လုပ်ခြင်း

Program.cs

```csharp
builder.Services.AddDbContext<GameStoreDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"));
});
```

---

# အများဆုံးတွေ့ရသော Errors များ

## 1. Login Failed Error

```text
Login failed for user 'sa'
```

### စစ်ဆေးရန်

* SQL Authentication ဖွင့်ထားသလား
* Password မှန်သလား
* sa Account Enable ဖြစ်သလား

---

## 2. EF Tool မရှိခြင်း

```text
Could not execute because the specified command or file was not found.
```

### ဖြေရှင်းနည်း

```bash
dotnet tool install --global dotnet-ef
```

---

## 3. Design Package မရှိခြင်း

```text
Your startup project doesn't reference Microsoft.EntityFrameworkCore.Design
```

### ဖြေရှင်းနည်း

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 4. Target Project Doesn't Match Migrations Assembly

```text
Your target project 'Api'
doesn't match your migrations assembly 'Database'
```

### ဖြေရှင်းနည်း

```powershell
--project GameStore.Database
--startup-project GameStore.Api
```

သို့မဟုတ်

```csharp
options.UseSqlServer(
    connectionString,
    b => b.MigrationsAssembly("GameStore.Database"));
```

---

# အသုံးဝင်သော Commands များ

## EF Version စစ်ခြင်း

```bash
dotnet ef --version
```

## Installed Packages စစ်ခြင်း

```bash
dotnet list package
```

## Models များ ပြန် Generate လုပ်ခြင်း

```powershell
dotnet ef dbcontext scaffold `
"connection-string" `
Microsoft.EntityFrameworkCore.SqlServer `
-o Models `
-c GameStoreDbContext `
-f
```

---

# မှတ်ထားသင့်သော အချက်များ

* Scaffolding သည် Database First Development အတွက် အသုံးပြုသည်။
* Database Schema ပြောင်းလဲသွားပါက Scaffold Command ကို ပြန် Run ရမည်။
* `-f` သည် ရှိပြီးသား Files များကို Overwrite လုပ်ပေးသည်။
* Generated Files များကို တိုက်ရိုက်ပြင်ခြင်းထက် Partial Class အသုံးပြုခြင်းက ပိုကောင်းသည်။
* DbContext နှင့် Entity Models များကို Database မှ Auto Generate လုပ်ပေးနိုင်သည်။
