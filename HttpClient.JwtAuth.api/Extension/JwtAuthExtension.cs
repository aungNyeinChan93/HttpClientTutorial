using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HttpClient.JwtAuth.api.Extension
{
    public static class JwtAuthExtension
    {
        public static IServiceCollection MapJwtAuth(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30) 
                    };
                });


            services.AddAuthorization();


            return services;
        }
    }
}
