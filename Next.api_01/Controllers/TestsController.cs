using Httpclient.AuthDatabase.Models;
using HttpClient.Database;
using HttpClient.GameStoreDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Next.api_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly GameStoreDbContext _gameStoreContext;
        private readonly AuthDatabase _authContext;

        public TestsController(AppDbContext appDbContext, GameStoreDbContext gameStoreContext, AuthDatabase authContext)
        {
            _appDbContext = appDbContext;
            _gameStoreContext = gameStoreContext;
            _authContext = authContext;
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
    }
}
