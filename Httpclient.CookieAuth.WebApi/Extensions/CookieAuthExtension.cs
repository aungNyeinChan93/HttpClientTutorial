using Microsoft.AspNetCore.Authentication.Cookies;

namespace Httpclient.CookieAuth.WebApi.Extensions
{
    public static class CookieAuthExtension
    {
        public static IServiceCollection MapCookieAuth(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/auth/login";
                    options.AccessDeniedPath = "/api/auth/denied";
                    options.Cookie.Name = "AppCookie";
                    options.ExpireTimeSpan = TimeSpan.FromHours(12);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });

            return services;
        }
    }
}
