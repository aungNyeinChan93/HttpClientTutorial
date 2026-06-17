# ASP.NET Core Logging with Serilog

## Overview

**Serilog** is a popular structured logging library for .NET applications. It provides powerful logging capabilities, supports multiple outputs (sinks), and produces searchable structured logs.

### Benefits of Serilog

* Structured Logging
* Multiple Log Destinations (Console, File, Database, Elasticsearch, Seq, etc.)
* Better Debugging and Monitoring
* Easy Integration with ASP.NET Core
* High Performance

---

# Installation

Install the required NuGet packages:

```bash
dotnet add package Serilog.AspNetCore

dotnet add package Serilog.Sinks.Console

dotnet add package Serilog.Sinks.File

dotnet add package Serilog.Settings.Configuration
```

---

# Basic Configuration

## Program.cs

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.MapGet("/", () => "Hello World");

app.Run();
```

---

# Logging in Controllers

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(
        ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Get Users Called");

        return Ok();
    }
}
```

---

# Log Levels

```csharp
_logger.LogTrace("Trace Message");

_logger.LogDebug("Debug Message");

_logger.LogInformation("Information Message");

_logger.LogWarning("Warning Message");

_logger.LogError("Error Message");

_logger.LogCritical("Critical Message");
```

| Level       | Description                       |
| ----------- | --------------------------------- |
| Trace       | Detailed diagnostic information   |
| Debug       | Development debugging information |
| Information | General application flow          |
| Warning     | Potential issue detected          |
| Error       | Error occurred                    |
| Critical    | Application failure               |

---

# Exception Logging

```csharp
try
{
    int result = 10 / 0;
}
catch (Exception ex)
{
    _logger.LogError(ex,
        "Error while calculating");
}
```

Example Output:

```text
[ERR] Error while calculating
System.DivideByZeroException:
Attempted to divide by zero.
```

---

# Structured Logging

## ❌ Bad Practice

```csharp
_logger.LogInformation(
    $"User {userId} logged in");
```

## ✅ Good Practice

```csharp
_logger.LogInformation(
    "User {UserId} logged in",
    userId);
```

### Why?

* Searchable Logs
* Better Performance
* Easier Analysis
* Compatible with Log Aggregation Tools

---

# Configuration with appsettings.json

## appsettings.json

```json
{
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/log-.txt",
          "rollingInterval": "Day"
        }
      }
    ]
  }
}
```

## Program.cs

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
```

---

# Request Logging Middleware

Automatically log HTTP requests:

```csharp
app.UseSerilogRequestLogging();
```

Example:

```text
HTTP GET /api/users responded 200 in 45ms
```

---

# Production Configuration

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();
```

### Features

* Console Logging
* Daily Rolling Files
* 30 Days Retention
* Structured Logs

---

# Recommended Folder Structure

```text
ProjectRoot
│
├── Logs
│   ├── log-2026-06-16.txt
│   ├── log-2026-06-17.txt
│
├── Program.cs
├── appsettings.json
└── Controllers
```

---

# .gitignore

Exclude log files from source control:

```gitignore
Logs/
```

---

# Best Practices

### Use Dependency Injection

```csharp
private readonly ILogger<UserService> _logger;
```

### Use Structured Logging

```csharp
_logger.LogInformation(
    "Order {OrderId} created",
    orderId);
```

### Log Exceptions

```csharp
_logger.LogError(
    ex,
    "Order creation failed");
```

### Enable Request Logging

```csharp
app.UseSerilogRequestLogging();
```

### Keep Logs Out of Git

```gitignore
Logs/
```

---

# Quick Setup

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Settings.Configuration
```

```csharp
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
});

app.UseSerilogRequestLogging();
```

This setup provides:

* Console Logging
* File Logging
* Structured Logging
* Request Logging
* Production Ready Configuration

```
```
