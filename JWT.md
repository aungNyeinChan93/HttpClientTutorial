# JWT Authentication (Access Token + Refresh Token)

## JWT Authentication ဆိုတာဘာလဲ?

JWT (JSON Web Token) ဆိုတာ User Login ဝင်ပြီးနောက် Authentication အတွက် အသုံးပြုတဲ့ Token Based Authentication System တစ်ခုဖြစ်ပါတယ်။

ဒီ Project မှာ -

* Access Token
* Refresh Token
* Role Claim
* SQL Server Storage

တို့ကို အသုံးပြုထားပါတယ်။

---

# Authentication Flow

```text
Login
  ↓
Generate Access Token (15 Minutes)
  ↓
Generate Refresh Token (7 Days)
  ↓
Save Refresh Token To Database
  ↓
Client Call Protected APIs
  ↓
Access Token Expired
  ↓
Send Refresh Token
  ↓
Generate New Access Token
```

---

# 1. JWT Configuration

## Purpose

JWT Token Generate လုပ်တဲ့အခါ

* Secret Key
* Issuer
* Audience

တို့ကို အသုံးပြုပါတယ်။

### appsettings.json

```json
{
  "Jwt": {
    "Key": "YourSecretKey",
    "Issuer": "MyApi",
    "Audience": "MyClient"
  }
}
```

---

# 2. JWT Authentication Registration

## Purpose

Application Startup မှာ JWT Authentication Service ကို Register လုပ်ပေးရပါတယ်။

### Code

```csharp
builder.Services.MapJwtAuth(builder.Configuration);
```

---

### JwtAuthExtension.cs

```csharp
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],

                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],

                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration["Jwt:Key"]!)),

                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromSeconds(30)
            };
    });
```

### Explanation

Token ဝင်လာတဲ့အခါ

* Issuer မှန်မမှန်
* Audience မှန်မမှန်
* Signature မှန်မမှန်
* Expire ဖြစ်မဖြစ်

စစ်ဆေးပေးပါတယ်။

---

# 3. Login Process

## Purpose

User Login ဝင်တဲ့အခါ

* User ရှိမရှိ စစ်
* Access Token Generate
* Refresh Token Generate
* Database ထဲ Save

လုပ်ပါတယ်။

### Code

```csharp
var token = await GenerateTokenAsync(user);

var refreshToken = GenerateRefreshToken();

await SaveRefreshTokenAsync(new JwtRefreshTokenRequest
{
    UserId = user.Id,
    RefreshToken = refreshToken
});
```

---

### Response

```json
{
  "accessToken": "...",
  "refreshToken": "...",
  "user": {}
}
```

---

# 4. Access Token Generate

## Purpose

User Information ကို Claims အဖြစ် Token ထဲထည့်ပေးပါတယ်။

### Code

```csharp
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier,
        user.Id.ToString()),

    new Claim(ClaimTypes.Name,
        user.Name),

    new Claim(ClaimTypes.Email,
        user.Email),

    new Claim(ClaimTypes.Role,
        role?.Name ?? "user")
};
```

---

### Explanation

Token ထဲမှာ

```text
User Id
User Name
Email
Role
```

တို့ပါဝင်သွားပါတယ်။

---

### Token Expiration

```csharp
expires: DateTime.UtcNow.AddMinutes(15)
```

Token သက်တမ်း

```text
15 Minutes
```

ဖြစ်ပါတယ်။

---

# 5. Refresh Token Generate

## Purpose

Access Token Expire ဖြစ်သွားတဲ့အခါ အသစ်ထုတ်နိုင်ဖို့ Refresh Token အသုံးပြုပါတယ်။

### Code

```csharp
var randomBytes = new byte[64];

using var rng =
    RandomNumberGenerator.Create();

rng.GetBytes(randomBytes);

return Convert.ToBase64String(randomBytes);
```

---

### Explanation

Refresh Token ကို

* Random
* Unique
* Hard To Guess

ဖြစ်အောင် Generate လုပ်ထားပါတယ်။

---

# 6. Save Refresh Token

## Purpose

Refresh Token ကို Database ထဲသိမ်းထားပါတယ်။

### Code

```csharp
var token = new RefreshToken
{
    UserId = request.UserId,
    Token = request.RefreshToken,
    CreatedAt = DateTime.UtcNow,
    ExpiresAt = DateTime.UtcNow.AddDays(7)
};
```

---

### Explanation

Refresh Token သက်တမ်း

```text
7 Days
```

ဖြစ်ပါတယ်။

---

# 7. Generate New Access Token

## Purpose

Access Token Expire ဖြစ်သွားတဲ့အခါ Refresh Token နဲ့ Access Token အသစ်ပြန်ထုတ်ပေးပါတယ်။

### Step 1

Refresh Token ကိုရှာ

```csharp
var storeToken =
    await _context.RefreshTokens
        .FirstOrDefaultAsync(
            x => x.Token == request.RefreshToken);
```

---

### Step 2

Expire ဖြစ်မဖြစ်စစ်

```csharp
if(storeToken.ExpiresAt < DateTime.UtcNow)
{
    // Expired
}
```

---

### Step 3

User ရှာ

```csharp
var user =
    await _context.Users
        .Where(x => x.Id == storeToken.UserId)
        .Select(x => x.Change())
        .FirstOrDefaultAsync();
```

---

### Step 4

Token အသစ်ထုတ်

```csharp
var token =
    await GenerateTokenAsync(user);
```

---

# 8. Middleware Configuration

### Program.cs

```csharp
app.UseAuthentication();

app.UseAuthorization();
```

### Important

Authentication ကို Authorization အရင်ရေးရပါတယ်။

✅ Correct

```csharp
app.UseAuthentication();

app.UseAuthorization();
```

❌ Wrong

```csharp
app.UseAuthorization();

app.UseAuthentication();
```

---

# 9. Authorize Attribute

### Login Required

```csharp
[Authorize]
public IActionResult Profile()
{
    return Ok();
}
```

---

### Admin Only

```csharp
[Authorize(Roles = "Admin")]
public IActionResult Dashboard()
{
    return Ok();
}
```

---

# Database Table

```sql
CREATE TABLE RefreshTokens
(
    Id INT IDENTITY PRIMARY KEY,

    UserId INT NOT NULL,

    Token NVARCHAR(MAX),

    CreatedAt DATETIME,

    ExpiresAt DATETIME
);
```

---

# Security Best Practices

### Use Strong Secret Key

```json
{
  "Jwt": {
    "Key": "VeryLongSecretKey"
  }
}
```

### Use HTTPS

```csharp
app.UseHttpsRedirection();
```

### Store Refresh Token In Database

Memory ထဲမဟုတ်ဘဲ Database ထဲသိမ်းပါ။

### Use UTC Time

```csharp
DateTime.UtcNow
```

အသုံးပြုပါ။

---

# Summary

ဒီ Project မှာ

✅ JWT Authentication

✅ Access Token

✅ Refresh Token

✅ Role Claims

✅ Token Validation

✅ SQL Server Storage

✅ ASP.NET Core Web API

တို့ကို အသုံးပြုပြီး Secure Authentication System တည်ဆောက်ထားပါတယ်။
