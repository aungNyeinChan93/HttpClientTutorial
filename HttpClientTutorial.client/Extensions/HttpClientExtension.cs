using HttpClientTutorial.client.Features;

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

            return services;
        }
    }
}
