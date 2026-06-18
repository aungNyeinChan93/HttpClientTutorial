using Httpclient.AuthDatabase.Models;
using HttpClient.domain.Features.JwtAuth;
using HttpClient.JwtAuth.api.Extension;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuthDatabase>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB"));
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//JWT
builder.Services.MapJwtAuth(builder.Configuration);
builder.Services.AddScoped<JwtAuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
