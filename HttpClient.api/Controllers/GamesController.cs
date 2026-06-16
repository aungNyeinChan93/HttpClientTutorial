using HttpClient.domain.Features.Game;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HttpClient.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly GameService _gameService;

        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _gameService.GetAllAsync();
            return Ok(response);
        }
    }
}
