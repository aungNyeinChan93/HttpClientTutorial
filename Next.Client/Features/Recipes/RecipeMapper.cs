using Next.Client.Features.Recipes.Models;
using Next.Domain.Features.ApiServices.Dummy.Recipes.Models;

namespace Next.Client.Features.Recipes
{
    public static class RecipeMapper
    {
        public static RecipeDto ToChange(this Recipe recipe)
        {
            return new RecipeDto
            {
                id = recipe.id,
                name = recipe.name,
                caloriesPerServing = recipe.caloriesPerServing,
                cookTimeMinutes = recipe.cookTimeMinutes,
                cuisine = recipe.cuisine,
                difficulty = recipe.difficulty,
                image = recipe.image,
                ingredients = recipe.ingredients,
                instructions = recipe.instructions,
                mealType = recipe.mealType,
                prepTimeMinutes = recipe.prepTimeMinutes,
                reviewCount = recipe.reviewCount,
                rating = recipe.rating,
                servings = recipe.servings,
                tags = recipe.tags,
                userId = recipe.userId,
            };
        }
    }
}
