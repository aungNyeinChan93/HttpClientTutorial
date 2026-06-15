# HttpClientTutorial

# HttpClient, IHttpClientFactory, Named Client, Typed Client, Refit Guide

## Overview

ASP.NET Core / Blazor မှာ External API တွေကို ခေါ်ဖို့ `HttpClient` ကို အသုံးပြုကြပါတယ်။

Modern .NET Application တွေမှာ

- HttpClient
- IHttpClientFactory
- Named Client
- Typed Client
- Refit

တို့ကို အသုံးပြုကြပါတယ်။

---

# 1. HttpClient (Basic)

အရိုးရှင်းဆုံးနည်း

```csharp
public class ProductService
{
    private readonly HttpClient _httpClient = new();

    public async Task<string> GetProducts()
    {
        return await _httpClient.GetStringAsync(
            "https://localhost:7071/api/products");
    }
}
```

## Problems

- Socket Exhaustion
- DNS Refresh Issue
- Memory Usage
- Hard to Manage

Production မှာ မသုံးသင့်ပါ။

---

# 2. IHttpClientFactory

Microsoft Recommended Approach

## Register

```csharp
builder.Services.AddHttpClient();
```

## Usage

```csharp
public class ProductService
{
    private readonly IHttpClientFactory _factory;

    public ProductService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<string> GetProducts()
    {
        var client = _factory.CreateClient();

        return await client.GetStringAsync(
            "https://localhost:7071/api/products");
    }
}
```

## Benefits

- Connection Pooling
- DNS Refresh Handling
- Better Memory Management
- Dependency Injection Support

---

# 3. Named Client

API အများကြီးရှိတဲ့အခါ အသုံးပြုသည်။

## Register

```csharp
builder.Services.AddHttpClient("ProductApi", client =>
{
    client.BaseAddress =
        new Uri("https://localhost:7071/api/");
});

builder.Services.AddHttpClient("OrderApi", client =>
{
    client.BaseAddress =
        new Uri("https://localhost:7072/api/");
});
```

## Usage

```csharp
public class ProductService
{
    private readonly IHttpClientFactory _factory;

    public ProductService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<string> GetProducts()
    {
        var client =
            _factory.CreateClient("ProductApi");

        return await client.GetStringAsync(
            "products");
    }
}
```

## When to Use

Suitable when:

- Product API
- User API
- Order API
- Payment API

are separated.

---

# 4. Typed Client

Production Projects တွေမှာ အသုံးအများဆုံး။

## Service

```csharp
public class ProductApiService
{
    private readonly HttpClient _httpClient;

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _httpClient
            .GetFromJsonAsync<List<Product>>
            ("products");
    }
}
```

## Register

```csharp
builder.Services.AddHttpClient<ProductApiService>(client =>
{
    client.BaseAddress =
        new Uri("https://localhost:7071/api/");
});
```

## Inject

```razor
@inject ProductApiService ProductApiService
```

or

```csharp
public class ProductUseCase
{
    private readonly ProductApiService _productApi;

    public ProductUseCase(ProductApiService productApi)
    {
        _productApi = productApi;
    }
}
```

---

# Typed Client Internals

Typed Client က IHttpClientFactory ကို မသုံးတာ မဟုတ်ပါ။

အတွင်းမှာ

```csharp
var client =
    httpClientFactory.CreateClient();
```

ကို ASP.NET Core က Auto Execute လုပ်ပေးပါတယ်။

Conceptually

```csharp
builder.Services.AddHttpClient<ProductApiService>();
```

is similar to

```csharp
builder.Services.AddTransient<ProductApiService>(sp =>
{
    var factory =
        sp.GetRequiredService<IHttpClientFactory>();

    var client =
        factory.CreateClient();

    return new ProductApiService(client);
});
```

---

# Typed Client Registration

## Correct

```csharp
builder.Services.AddHttpClient<ProductApiService>();
```

## Incorrect

```csharp
builder.Services.AddScoped<ProductApiService>();

builder.Services.AddHttpClient<ProductApiService>();
```

AddScoped ထပ်ထည့်ရန် မလိုပါ။

---

# Multiple API Services

ဥပမာ

```csharp
builder.Services.AddHttpClient<ProductApiService>();
builder.Services.AddHttpClient<OrderApiService>();
builder.Services.AddHttpClient<UserApiService>();
```

လုပ်နိုင်ပါတယ်။

100 Endpoint ရှိလည်း

100 Service ဖြစ်စရာမလိုပါ။

ဥပမာ

Product API

```text
GET    /products
GET    /products/{id}
POST   /products
PUT    /products/{id}
DELETE /products/{id}
```

အားလုံးကို

```csharp
ProductApiService
```

တစ်ခုတည်းမှာ Handle လုပ်နိုင်ပါတယ်။

---

# Clean Registration

## Extension Method

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClients(
        this IServiceCollection services)
    {
        services.AddHttpClient<ProductApiService>();
        services.AddHttpClient<OrderApiService>();
        services.AddHttpClient<UserApiService>();

        return services;
    }
}
```

Program.cs

```csharp
builder.Services.AddApiClients();
```

---

# 5. Refit

Refit = Automatic REST API Client Generator

## Install

```bash
dotnet add package Refit
```

---

## Interface

```csharp
using Refit;

public interface IProductApi
{
    [Get("/products")]
    Task<List<Product>> GetProducts();
}
```

---

## Register

```csharp
builder.Services
    .AddRefitClient<IProductApi>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress =
            new Uri("https://localhost:7071/api");
    });
```

---

## Usage

```csharp
public class ProductService
{
    private readonly IProductApi _api;

    public ProductService(IProductApi api)
    {
        _api = api;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _api.GetProducts();
    }
}
```

---

# Refit CRUD Example

```csharp
public interface IProductApi
{
    [Get("/products")]
    Task<List<Product>> GetProducts();

    [Get("/products/{id}")]
    Task<Product> GetProduct(int id);

    [Post("/products")]
    Task CreateProduct(Product product);

    [Put("/products/{id}")]
    Task UpdateProduct(int id, Product product);

    [Delete("/products/{id}")]
    Task DeleteProduct(int id);
}
```

---

# Configuration

## appsettings.json

```json
{
  "ApiEndPoint": {
    "Quotes": "https://localhost:7071/api/quotes",
    "Products": "https://localhost:7071/api/products"
  }
}
```

---

# IConfiguration Access

## Direct Access

```csharp
var quoteApi =
    configuration["ApiEndPoint:Quotes"];
```

---

## Program.cs

```csharp
builder.Services.AddHttpClient<QuoteApiService>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiEndPoint:Quotes"]!
    );
});
```

---

# Options Pattern

## Options Class

```csharp
public class ApiEndPointOptions
{
    public string Quotes { get; set; } = string.Empty;

    public string Products { get; set; } = string.Empty;
}
```

## Register

```csharp
builder.Services.Configure<ApiEndPointOptions>(
    builder.Configuration.GetSection("ApiEndPoint"));
```

## Use

```csharp
public class QuoteService
{
    private readonly ApiEndPointOptions _options;

    public QuoteService(
        IOptions<ApiEndPointOptions> options)
    {
        _options = options.Value;
    }

    public string GetQuoteApi()
    {
        return _options.Quotes;
    }
}
```

---

# Architecture Recommendation

## Small Projects

```text
Named Client
```

or

```text
Typed Client
```

---

## Medium Projects

```text
Typed Client
+
Repository Pattern
```

---

## Large Projects

```text
Refit
+
API Gateway (YARP)
+
Microservices
```

---

# Production Architecture

```text
Blazor UI
    ↓
UseCase
    ↓
Repository
    ↓
Typed Client / Refit
    ↓
Web API
    ↓
Database
```

---

# Summary

| Feature | Recommended |
|----------|-------------|
| HttpClient | ❌ No |
| IHttpClientFactory | ✅ Yes |
| Named Client | ✅ Good |
| Typed Client | ✅ Best for Most Projects |
| Refit | ✅ Best for Large Projects |

## Most Common Choice

```text
Blazor Server
      ↓
Typed Client
      ↓
ASP.NET Core Web API
```

This is the pattern most commonly used in modern ASP.NET Core applications.
