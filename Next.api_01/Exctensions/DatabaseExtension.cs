using Httpclient.AuthDatabase.Models;
using HttpClient.Database;
using HttpClient.GameStoreDb.Models;
using Microsoft.EntityFrameworkCore;

namespace Next.api_01.Exctensions
{
    public static class DatabaseExtension
    {
       
        public static WebApplicationBuilder MapDatabase(this WebApplicationBuilder builder)
        {
            builder.AddDefaultDb()
                .AddGameStoreDb()
                .AddAuthDb();

            return builder;
        }

        public static WebApplicationBuilder AddDefaultDb(this WebApplicationBuilder builder )
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")); 
            });
            return builder;
        }

        public static WebApplicationBuilder AddAuthDb(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDatabase>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDb"));
            });
            return builder;
        }

        public static WebApplicationBuilder AddGameStoreDb(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<GameStoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("GameStore"));
            });
            return builder;
        }
    }
}
