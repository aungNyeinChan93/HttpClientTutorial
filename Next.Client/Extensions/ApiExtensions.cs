using Next.Client.Features.Recipes;

namespace Next.Client.Extensions
{
    public static class ApiExtensions
    {
        public static WebApplicationBuilder MapApiServices(this WebApplicationBuilder builder)
        {
            builder.AddPostApi();
            return builder;
        }

        public static WebApplicationBuilder AddPostApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<RecipeService>(options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["ApiUrl:Main"]!);
            });

            return builder;
        }
    }
}
