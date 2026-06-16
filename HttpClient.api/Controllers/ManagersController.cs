using HttpClient.domain.Features.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClient.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var response = await _managerService.GetAllAsync();
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    return StatusCode(400,response.Message);
                }
                if (response.IsNotFound)
                {
                    return StatusCode(404, response.Message);
                }
                if (response.IsDataError)
                {
                    return StatusCode(500, response.Message);
                }
                if (!response.IsSystemError)
                {
                    return StatusCode(500, response.Message);

                }
            }
            return Ok(response);

        }
    }
}
