using HttpClient.Database;
using HttpClient.domain.Features.Game;
using HttpClient.domain.Features.Manager;
using HttpClient.GameStoreDb.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddDbContext<GameStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameStore"));
});

builder.Services.AddScoped<IManagerService,ManagerService>();
builder.Services.AddScoped<GameService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
