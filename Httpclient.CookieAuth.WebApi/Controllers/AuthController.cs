using HttpClient.domain.Features.Auth;
using HttpClient.domain.Features.Auth.ReqResModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (request.Name !="aung" || request.Email != "aung@123")
            {
                return Unauthorized();
            }

            var response = await _authservice.Login(request);
            if (response.IsError)
            {
                return StatusCode(500,response.Message);
            }


            var authProperties = new AuthenticationProperties
            {
                IsPersistent =true, // "Remember me" option
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,response.Data.ClaimsPrincipal,authProperties);

            return Ok(response.Message);

        }
       
    }
}
