using Next.Domain.Features.ApiServices.Dummy.Posts;
using Next.Domain.Features.ApiServices.Dummy.Recipes;

namespace Next.api_01.Exctensions
{
    public static class ExternalApiExtensions
    { 
        public static WebApplicationBuilder MapExternalApi(this WebApplicationBuilder builder)
        {
            builder.AddDummyApi();
            return builder;
        }

        public static WebApplicationBuilder AddDummyApi(this WebApplicationBuilder builder)
        {

            builder.Services.AddHttpClient<PostApiService>(options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["ExternalApi:dummy"]!);
            });

            builder.Services.AddHttpClient<RecipeApiService>(options =>
            {
                options.BaseAddress = new Uri(builder.Configuration["ExternalApi:dummy"]!);
            });

            return builder;
        }
    }
}
