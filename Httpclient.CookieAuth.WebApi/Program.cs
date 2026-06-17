using Httpclient.AuthDatabase.Models;
using Httpclient.CookieAuth.WebApi.Extensions;
using HttpClient.domain.Features.Auth;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AuthDatabase>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//CookieAuth
builder.Services.MapCookieAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService,AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();


app.MapGet("/api/users", (AuthDatabase db) =>
{
    var res = db.Users.AsNoTracking().ToList();
    return res;
});

app.Run();
