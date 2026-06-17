using HttpClientTutorial.client.Features.Games;
using HttpClientTutorial.client.Features.Manager;
using HttpClientTutorial.client.Features.Quote;
using HttpClientTutorial.client.Features.User;

namespace HttpClientTutorial.client.Extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection MapApi(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpClient<QuoteApiService>(options =>
              {
                  options.BaseAddress = new Uri(configuration["ApiEndPoint:Dummy"]!);
              });

            services.AddHttpClient<ManagerApiService>(options =>
            {
                options.BaseAddress = new Uri(configuration["ApiEndPoint:Client"]!);
            });

            services.AddHttpClient<GameApiService>(options =>
            {
                options.BaseAddress = new Uri($"{configuration["ApiEndPoint:Game"]}");
            });

            services.AddHttpClient<UserApiService>(options =>
            {
                options.BaseAddress = new Uri(configuration["ApiEndPoint:Client2"]!);
            });

            return services;
        }
    }
}
