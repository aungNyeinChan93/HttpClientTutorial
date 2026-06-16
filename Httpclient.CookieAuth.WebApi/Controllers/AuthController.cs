using HttpClient.domain.Features.Auth;
using HttpClient.domain.Features.Auth.ReqResModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Httpclient.CookieAuth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authservice;

        public AuthController(AuthService authservice)
        {
            _authservice = authservice;
        }


        //Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authservice.Login(request);

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

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // "Remember me" option
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, response.Data.ClaimsPrincipal, authProperties);

            return Ok(response.Message);

        }

        //Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await _authservice.Register(request);
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

        [Authorize]
        //TestAuth
        [HttpGet("isAuth")]
        public async Task<IActionResult> IsAuth()
        {
            var userName = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
            return Ok(userName);
        }


        //Logout
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var isAuthenticate = User.Identity!.IsAuthenticated;
            if (isAuthenticate)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok("logout success");
            }
            return BadRequest();
        }
    }
}
