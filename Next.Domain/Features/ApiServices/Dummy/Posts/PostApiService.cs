using HttpClient.shared.Commons;
using Next.Domain.Features.ApiServices.Dummy.Posts.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Next.Domain.Features.ApiServices.Dummy.Posts
{
    public class PostApiService
    {
        private readonly System.Net.Http.HttpClient _httpClient;

       public PostApiService(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        // Get All Posts
        public async Task<Result<PostsResponse>> GetAllPostsAsync()
        {
            Result<PostsResponse> responseModel = new();
            var response = await _httpClient.GetFromJsonAsync<PostsResponse>("/posts");

            if (response is null)
            {
                responseModel = Result<PostsResponse>.NotFoundError();
                return responseModel;
            }

            responseModel = Result<PostsResponse>.Success(response,"Get All Posts");
            return responseModel;
        }
}}
