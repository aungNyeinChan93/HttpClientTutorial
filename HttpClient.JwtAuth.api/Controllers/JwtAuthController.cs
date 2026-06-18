using HttpClient.domain.Features.JwtAuth;
using HttpClient.domain.Features.JwtAuth.ReqResModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClient.JwtAuth.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
    {
        private readonly JwtAuthService _jwtAuthService;
        private readonly ILogger<JwtAuthController> _logger;

        public JwtAuthController(JwtAuthService jwtAuthService, ILogger<JwtAuthController> logger)
        {
            _jwtAuthService = jwtAuthService;
            _logger = logger;
        }

        [HttpPost]
        [Route("jwt-login")]
        public async Task<IActionResult> Login(JwtLoginRequest request)
        {
            var response = await _jwtAuthService.LoginAsync(request);
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(400, response.Message);
                }
                if (response.IsNotFound)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(404, response.Message);
                }
                if (response.IsDataError)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(500, response.Message);
                }
                if (!response.IsSystemError)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(500, response.Message);
                }
            }

            return Ok(response);
        }

        //GenerateAccessToken
        [HttpPost]
        [Route("generateAccessToken")]
        public async Task<IActionResult> GenerateAccessToken(GenerateAccessTokenRequest request)
        {
            var response = await _jwtAuthService.GenerateAccessTokenByRefreshTokenAsync(request);
            if (response.IsError)
            {
                if (response.IsDataError)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(400, response.Message);
                }
                if (response.IsNotFound)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(404, response.Message);
                }
                if (response.IsDataError)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(500, response.Message);
                }
                if (!response.IsSystemError)
                {
                    _logger.LogError($"[JwtAuthController] {response.Message}");
                    return StatusCode(500, response.Message);
                }
            }

            return Ok(response);
        }

    }
}
