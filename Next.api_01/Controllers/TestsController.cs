using Httpclient.AuthDatabase.Models;
using HttpClient.Database;
using HttpClient.GameStoreDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Next.Domain.Features.ApiServices.Dummy.Posts;
using Next.Domain.Features.ApiServices.Dummy.Recipes;

namespace Next.api_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly GameStoreDbContext _gameStoreContext;
        private readonly AuthDatabase _authContext;
        private readonly PostApiService _postApiService;
        private readonly RecipeApiService _recipeApiService;

        public TestsController(AppDbContext appDbContext, GameStoreDbContext gameStoreContext, AuthDatabase authContext, PostApiService postApiService, RecipeApiService recipeApiService)
        {
            _appDbContext = appDbContext;
            _gameStoreContext = gameStoreContext;
            _authContext = authContext;
            _postApiService = postApiService;
            _recipeApiService = recipeApiService;
        }

        [HttpGet]
        [Route("AppDb")]
        public async Task<IActionResult> TestAppDb()
        {
            var response = await _appDbContext.Managers.AsNoTracking().ToListAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("AuthDB")]
        public async Task<IActionResult> TestAuthDb()
        {
            var response = await _authContext.Users.AsNoTracking().ToListAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("GameStoreDb")]
        public async Task<IActionResult> TestGameStoreDb()
        {
            var response = await _gameStoreContext.Games.AsNoTracking().ToListAsync();
            return Ok(response);
        }


        [HttpGet]
        [Route("dummy/posts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var response = await _postApiService.GetAllPostsAsync();
            return Ok(response);
        }


        [HttpGet]
        [Route("dummy/recipes")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var response = await _recipeApiService.GetAllRecipesAsync();
            return Ok(response);
        }
    }
}
