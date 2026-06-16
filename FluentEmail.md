# FluentEmail Usage Guide (ASP.NET Core)

## What is FluentEmail?

FluentEmail သည် .NET အတွက် Email Sending Library တစ်ခုဖြစ်ပြီး SMTP, SendGrid, MailGun စသည့် Email Providers များကို လွယ်ကူစွာ အသုံးပြုနိုင်ရန် Fluent API ပုံစံဖြင့် ရေးသားထားသော Library ဖြစ်သည်။

---

# Installation

## SMTP Only

```bash
dotnet add package FluentEmail.Core
dotnet add package FluentEmail.Smtp
```

## Razor Template Support

```bash
dotnet add package FluentEmail.Razor
```

---

# Service Registration

## Program.cs

```csharp
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;

var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
{
    Port = 587,
    EnableSsl = true,
    Credentials = new NetworkCredential(
        "your-email@gmail.com",
        "your-app-password")
});

builder.Services
    .AddFluentEmail("your-email@gmail.com")
    .AddSmtpSender(sender);
```

---

# Basic Usage

## Send Plain Text Email

```csharp
public class EmailService
{
    private readonly IFluentEmail _email;

    public EmailService(IFluentEmail email)
    {
        _email = email;
    }

    public async Task SendAsync()
    {
        await _email
            .To("user@gmail.com")
            .Subject("Welcome")
            .Body("Hello World")
            .SendAsync();
    }
}
```

---

# Send HTML Email

```csharp
await _email
    .To("user@gmail.com")
    .Subject("Welcome")
    .Body("<h1>Hello User</h1>", true)
    .SendAsync();
```

`true` ဆိုသည်မှာ HTML Content ဖြစ်ကြောင်း ပြောခြင်းဖြစ်သည်။

---

# Multiple Recipients

```csharp
await _email
    .To("user1@gmail.com")
    .To("user2@gmail.com")
    .Subject("Announcement")
    .Body("Hello Everyone")
    .SendAsync();
```

---

# CC

```csharp
await _email
    .To("user@gmail.com")
    .CC("manager@gmail.com")
    .Subject("Report")
    .Body("Monthly Report")
    .SendAsync();
```

---

# BCC

```csharp
await _email
    .To("user@gmail.com")
    .BCC("admin@gmail.com")
    .Subject("Notification")
    .Body("System Notification")
    .SendAsync();
```

---

# Attachment

```csharp
await _email
    .To("user@gmail.com")
    .Subject("Invoice")
    .Body("Please see attachment.")
    .AttachFromFilename(@"C:\Files\invoice.pdf")
    .SendAsync();
```

---

# Send Using Model

```csharp
public class WelcomeEmail
{
    public string Name { get; set; } = "";
}
```

```csharp
var model = new WelcomeEmail
{
    Name = "Nyein Chan"
};
```

---

# Razor Template

## Template File

```html
<h1>Hello @Model.Name</h1>

<p>
Welcome to our application.
</p>
```

---

## Send Template Email

```csharp
await _email
    .To("user@gmail.com")
    .Subject("Welcome")
    .UsingTemplateFromFile(
        "Templates/Welcome.cshtml",
        model)
    .SendAsync();
```

---

# Check Send Result

```csharp
var response = await _email
    .To("user@gmail.com")
    .Subject("Test")
    .Body("Hello")
    .SendAsync();
```

```csharp
if(response.Successful)
{
    Console.WriteLine("Email Sent");
}
else
{
    foreach(var error in response.ErrorMessages)
    {
        Console.WriteLine(error);
    }
}
```

---

# Recommended Email Service

```csharp
public interface IEmailService
{
    Task SendAsync(
        string to,
        string subject,
        string body);
}
```

```csharp
public class EmailService : IEmailService
{
    private readonly IFluentEmail _email;

    public EmailService(IFluentEmail email)
    {
        _email = email;
    }

    public async Task SendAsync(
        string to,
        string subject,
        string body)
    {
        var response = await _email
            .To(to)
            .Subject(subject)
            .Body(body, true)
            .SendAsync();

        if (!response.Successful)
        {
            throw new Exception(
                string.Join(", ",
                response.ErrorMessages));
        }
    }
}
```

---

# Best Practices

✅ Create `IEmailService`

✅ Use Dependency Injection

✅ Store SMTP Settings in `appsettings.json`

✅ Use Razor Templates for Email Layouts

✅ Run Email Sending in Hangfire Background Jobs

✅ Log Failed Emails

✅ Use Result Pattern Instead of Throwing Exceptions

```csharp
public async Task<Result> SendAsync(...)
{
    var response = await _email
        .To(to)
        .Subject(subject)
        .Body(body)
        .SendAsync();

    return response.Successful
        ? Result.Success()
        : Result.Failure(
            string.Join(", ",
            response.ErrorMessages));
}
```

---

# Common FluentEmail Methods

| Method                  | Description       |
| ----------------------- | ----------------- |
| To()                    | Recipient         |
| Subject()               | Email Subject     |
| Body()                  | Email Body        |
| CC()                    | Carbon Copy       |
| BCC()                   | Blind Carbon Copy |
| ReplyTo()               | Reply Address     |
| Attach()                | Add Attachment    |
| AttachFromFilename()    | Attach File       |
| UsingTemplate()         | Razor Template    |
| UsingTemplateFromFile() | Razor File        |
| SendAsync()             | Send Email        |

FluentEmail သည် ASP.NET Core Application များတွင် Email ပို့ရန်အတွက် Simple, Clean, Testable, Extendable Solution တစ်ခုဖြစ်သည်။
