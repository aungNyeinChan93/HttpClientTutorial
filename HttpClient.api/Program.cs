using HttpClient.Database;
using HttpClient.domain.Features.Email;
using HttpClient.domain.Features.Game;
using HttpClient.domain.Features.Manager;
using HttpClient.GameStoreDb.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddDbContext<GameStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameStore"));
});

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("client",policy =>
    {
        policy.WithOrigins("http://localhost:5246", "https://localhost:7189", "http://localhost:5246")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


//FluentEmail
string email = builder.Configuration["EmailSetting:Email"]!;
string emailAppPassword = builder.Configuration["EmailSetting:Password"]!;
builder.Services.AddFluentEmail(email)
    .AddSmtpSender("smtp.gmail.com", 587, email, emailAppPassword);


//Services
builder.Services.AddScoped<IManagerService,ManagerService>();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("client");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
