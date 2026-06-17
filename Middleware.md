# ASP.NET Core Middleware

## Middleware ဆိုတာဘာလဲ?

Middleware ဆိုတာ ASP.NET Core Application ထဲတွင် HTTP Request နှင့် HTTP Response များကို ကြားခံအဖြစ် လုပ်ဆောင်ပေးသော Component တစ်ခုဖြစ်သည်။

Client မှ Request တစ်ခုဝင်လာသောအခါ Middleware Pipeline ကို အစဉ်လိုက် ဖြတ်သန်းသွားပြီး Endpoint (Controller, Minimal API) သို့ ရောက်ရှိသည်။ Response ပြန်လာသောအခါလည်း Middleware များကို ပြန်လည်ဖြတ်သန်းပြီး Client သို့ ပို့ဆောင်ပေးသည်။

```text
Client
   │
   ▼
Middleware 1
   │
   ▼
Middleware 2
   │
   ▼
Middleware 3
   │
   ▼
Controller / Endpoint
   │
   ▼
Response
```

---

# Middleware Pipeline

ASP.NET Core တွင် Middleware များကို `Program.cs` ဖိုင်အတွင်း Configure ပြုလုပ်သည်။

```csharp
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Request Flow

```text
Request
  │
  ▼
UseHttpsRedirection
  │
  ▼
UseAuthentication
  │
  ▼
UseAuthorization
  │
  ▼
Controller
  │
  ▼
Response
```

---

# Built-in Middleware များ

## HTTPS Redirection

HTTP Request များကို HTTPS သို့ အလိုအလျောက် Redirect ပြုလုပ်ပေးသည်။

```csharp
app.UseHttpsRedirection();
```

---

## Static Files

`wwwroot` Folder အတွင်းရှိ File များကို Browser မှ Access လုပ်နိုင်အောင် Serve လုပ်ပေးသည်။

```csharp
app.UseStaticFiles();
```

ဥပမာ

```text
wwwroot/images/logo.png

https://localhost:5001/images/logo.png
```

---

## Authentication

User Login ဝင်ထားခြင်းရှိ/မရှိ စစ်ဆေးပေးသည်။

```csharp
app.UseAuthentication();
```

---

## Authorization

User Permission နှင့် Role များကို စစ်ဆေးပေးသည်။

```csharp
app.UseAuthorization();
```

---

## CORS

Frontend Application များမှ API ကို Access လုပ်နိုင်ရန် ခွင့်ပြုပေးသည်။

```csharp
app.UseCors();
```

---

## Exception Handling

Unhandled Exception များကို ဖမ်းယူပြီး Handle လုပ်ပေးသည်။

```csharp
app.UseExceptionHandler("/Error");
```

---

# Inline Middleware

Middleware Class သီးသန့်မရေးဘဲ `app.Use()` ဖြင့် အလွယ်တကူ ရေးနိုင်သည်။

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Before Request");

    await next();

    Console.WriteLine("After Response");
});
```

Output

```text
Before Request
After Response
```

---

# Custom Middleware

## Step 1 - Middleware Class ဖန်တီးခြင်း

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine(
            $"Request Path : {context.Request.Path}");

        await _next(context);
    }
}
```

---

## Step 2 - Middleware Register ပြုလုပ်ခြင်း

```csharp
app.UseMiddleware<RequestLoggingMiddleware>();
```

---

# Extension Method အသုံးပြုခြင်း

Middleware Register Code များကို သန့်ရှင်းစေရန် Extension Method အသုံးပြုနိုင်သည်။

```csharp
public static class MiddlewareExtensions
{
    public static IApplicationBuilder
        UseRequestLogging(
            this IApplicationBuilder app)
    {
        return app.UseMiddleware<
            RequestLoggingMiddleware>();
    }
}
```

အသုံးပြုပုံ

```csharp
app.UseRequestLogging();
```

---

# Run Middleware

Pipeline ကို ဆက်မသွားစေဘဲ Request ကို တိုက်ရိုက် Handle လုပ်လိုပါက `Run()` ကို အသုံးပြုနိုင်သည်။

```csharp
app.Run(async context =>
{
    await context.Response.WriteAsync(
        "Hello Middleware");
});
```

⚠️ `Run()` သည် Terminal Middleware ဖြစ်ပြီး Pipeline ကို အဆုံးသတ်ပေးသည်။

```csharp
app.Run(async context =>
{
    await context.Response.WriteAsync("Hello");
});

app.MapControllers(); // Execute မဖြစ်တော့ပါ
```

---

# Use(), Run(), Map() ကွာခြားချက်

| Method | Pipeline ဆက်သွားမလား | အသုံးပြုပုံ                      |
| ------ | -------------------- | -------------------------------- |
| Use()  | ✅ Yes                | Next Middleware ကို ခေါ်နိုင်သည် |
| Run()  | ❌ No                 | Pipeline ကို အဆုံးသတ်သည်         |
| Map()  | Route Branching      | Route အလိုက် Branch ခွဲနိုင်သည်  |

### Use()

```csharp
app.Use(async (ctx, next) =>
{
    await next();
});
```

### Run()

```csharp
app.Run(async ctx =>
{
    await ctx.Response.WriteAsync("End");
});
```

### Map()

```csharp
app.Map("/api", apiApp =>
{
    apiApp.Run(async ctx =>
    {
        await ctx.Response.WriteAsync(
            "API Branch");
    });
});
```

---

# Middleware Order အရေးကြီးပုံ

Middleware များသည် အစဉ်လိုက် Execute လုပ်သောကြောင့် Order မှန်ကန်ရမည်။

✅ မှန်ကန်သော Order

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

❌ မှားယွင်းသော Order

```csharp
app.UseAuthorization();
app.UseAuthentication();
```

Authentication မလုပ်ခင် Authorization စစ်ဆေးနေသောကြောင့် Error ဖြစ်နိုင်သည်။

---

# Real World Example

Request Logging Middleware

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(
            "Request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        await _next(context);
    }
}
```

Register

```csharp
app.UseMiddleware<RequestLoggingMiddleware>();
```

---

# Interview Questions

## Middleware ဆိုတာဘာလဲ?

HTTP Request နှင့် Response များကို Process လုပ်ပေးသော Component ဖြစ်သည်။

## RequestDelegate ဆိုတာဘာလဲ?

Next Middleware ကို Execute လုပ်ပေးသော Delegate ဖြစ်သည်။

## `await next()` မခေါ်ရင်ဘာဖြစ်မလဲ?

Request သည် ထို Middleware တွင်ပင် ရပ်သွားပြီး နောက် Middleware များသို့ မရောက်တော့ပါ။

## `Use()` နှင့် `Run()` ကွာခြားချက်?

* `Use()` → Next Middleware ကို ခေါ်နိုင်သည်။
* `Run()` → Pipeline ကို အဆုံးသတ်သည်။

## Middleware Order ဘာကြောင့် အရေးကြီးသလဲ?

Middleware များသည် အစဉ်လိုက် Execute လုပ်သောကြောင့် Order မှားလျှင် Authentication, Authorization, Routing စသည့် Feature များ မမှန်ကန်နိုင်ပါ။
