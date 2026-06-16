using Httpclient.AuthDatabase.Models;
using HttpClient.domain.Features.Auth.ReqResModels;
using HttpClient.shared.Commons;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HttpClient.domain.Features.Auth
{
    public class AuthService
    {
        private readonly AuthDatabase _context;


        public AuthService(AuthDatabase context)
        {
            _context = context;
            
        }

        #region Login
        public async Task<Result<LoginResponse>> Login(LoginRequest request)
        {
            var responseModel = new Result<LoginResponse>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,request.Name),
                new Claim(ClaimTypes.Email,request.Email),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            responseModel = Result<LoginResponse>.Success(new LoginResponse { ClaimsPrincipal = principal });

            return responseModel;
        }
        #endregion

    }
}
