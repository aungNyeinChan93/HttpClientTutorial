using HttpClient.domain.Features.Manager;
using HttpClient.domain.Features.Manager.ReqResModel;
using HttpClient.shared.Commons;
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


        //Get All Games
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var response = await _managerService.GetAllAsync();
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    return StatusCode(400, response.Message);
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

        //Create Game
        [HttpPost]
        public async Task<IActionResult> CreateGame(CreateManagerRequest request)
        {
            var response = await _managerService.CreateAsync(request);
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    return StatusCode(400, response.Message);
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

        //getById
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _managerService.GetByIdAsync(id);
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    return StatusCode(400, response.Message);
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

        //update Manager
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id,UpdateManagerRequest request)
        {
            var response = await _managerService.UpdateAsync(id, request);
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    return StatusCode(400, response.Message);
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

        //deleteManager
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _managerService.DeleteAsync(id);
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    return StatusCode(400, response.Message);
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
