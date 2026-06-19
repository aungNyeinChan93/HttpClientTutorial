using HttpClient.shared.Commons;
using Next.Domain.Features.ApiServices.Dummy.Recipes.Models;

namespace Next.Client.Features.Recipes
{
    public class RecipeService
    {
        public readonly System.Net.Http.HttpClient _httpClient;

        public RecipeService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<RecipesResponse>> GetAllRecipes()
        {
            var response = await _httpClient.GetFromJsonAsync<Result<RecipesResponse>>("/api/tests/dummy/recipes");
            return response!;
        }
    }
}
