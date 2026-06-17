using HttpClient.domain.Features.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Httpclient.CookieAuth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly UserService _userService;

        public UsersController(UserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();

            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    _logger.LogError("[UsersController:GetAllUsers] {mess}",response.Message);
                    return BadRequest(response.Message);
                }
                if (response.IsNotFound)
                {
                    _logger.LogError("[UsersController:GetAllUsers] {mess}", response.Message);
                    return BadRequest(response.Message);
                }
                if (response.IsInvalidData)
                {
                    _logger.LogError("[UsersController:GetAllUsers] {mess}", response.Message);
                    return BadRequest(response.Message);
                }
                if (response.IsValidationError)
                {
                    _logger.LogError("[UsersController:GetAllUsers] {mess}", response.Message);
                    return BadRequest(response.Message);
                }
                if (response.IsSystemError)
                {
                    _logger.LogError("[UsersController:GetAllUsers] {mess}", response.Message);
                    return BadRequest(response.Message);
                }
            }

            _logger.LogInformation("[UsersController:GetAllUsers] {message}",response.Message);
            return Ok(response);
        }
    }
}
