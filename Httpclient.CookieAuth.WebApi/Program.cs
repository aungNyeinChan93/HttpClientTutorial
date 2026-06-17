using Httpclient.AuthDatabase.Models;
using Httpclient.CookieAuth.WebApi.Extensions;
using Httpclient.CookieAuth.WebApi.Middlewares;
using HttpClient.domain.Features.Auth;
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



//CookieAuth
builder.Services.MapCookieAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<RequestLoggerMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggerMiddleware>();

app.UseSerilogRequestLogging();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();


app.MapGet("/api/users", (AuthDatabase db) =>
{
    var res = db.Users.AsNoTracking().ToList();
    return res;
});



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
