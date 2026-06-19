using HttpClient.shared.Commons;
using Next.Domain.Features.ApiServices.Dummy.Recipes.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Next.Domain.Features.ApiServices.Dummy.Recipes
{
    public class RecipeApiService
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public RecipeApiService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<RecipesResponse>> GetAllRecipesAsync()
        {
            Result<RecipesResponse> responseModel = new();
            var response = await _httpClient.GetFromJsonAsync<RecipesResponse>("/recipes");
            if (response is null)
            {
                responseModel = Result<RecipesResponse>.NotFoundError();
                return responseModel;
            }

            responseModel = Result<RecipesResponse>.Success(response);
            return responseModel;
    }
}}
