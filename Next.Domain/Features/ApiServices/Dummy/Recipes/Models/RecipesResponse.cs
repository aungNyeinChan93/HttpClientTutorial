using System;
using System.Collections.Generic;
using System.Text;

namespace Next.Domain.Features.ApiServices.Dummy.Recipes.Models
{

    public class RecipesResponse
    {
        public List<Recipe> recipes { get; set; } = new();
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }

    public class Recipe
    {
        public int id { get; set; }
        public string name { get; set; }
        public string[] ingredients { get; set; }
        public string[] instructions { get; set; }
        public int prepTimeMinutes { get; set; }
        public int cookTimeMinutes { get; set; }
        public int servings { get; set; }
        public string difficulty { get; set; }
        public string cuisine { get; set; }
        public int caloriesPerServing { get; set; }
        public string[] tags { get; set; }
        public int userId { get; set; }
        public string image { get; set; }
        public float rating { get; set; }
        public int reviewCount { get; set; }
        public string[] mealType { get; set; }
    }



}
