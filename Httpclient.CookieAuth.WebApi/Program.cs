using Httpclient.AuthDatabase.Models;
using Httpclient.CookieAuth.WebApi.Extensions;
using Httpclient.CookieAuth.WebApi.Middlewares;
using HttpClient.domain.Features.Auth;
using HttpClient.domain.Features.User;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AuthDatabase>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//Serilog
Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt",
                    rollingInterval: RollingInterval.Hour,
                    fileSizeLimitBytes: 10485760, // 10MB
                    retainedFileCountLimit: 24)
                .WriteTo.MSSqlServer(
                    connectionString: builder.Configuration.GetConnectionString("LogDb"),
                    tableName: "Logs",
                    autoCreateSqlTable: true) 
                .CreateLogger();

builder.Host.UseSerilog();


//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("client2", p =>
    {
        p.WithOrigins("http://localhost:5246", "https://localhost:7189")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


//CookieAuth
builder.Services.MapCookieAuth();
builder.Services.AddHttpContextAccessor();

//Midlewares
builder.Services.AddScoped<RequestLoggerMiddleware>();

//Services
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<UserService>();




//App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("client2");
app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggerMiddleware>();

app.UseSerilogRequestLogging();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();



try
{
    Log.Information("Application starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
