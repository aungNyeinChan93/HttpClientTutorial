using Httpclient.AuthDatabase.Models;
using HttpClient.domain.Features.Auth.ReqResModels;
using HttpClient.domain.Mappers;
using HttpClient.shared.Commons;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace HttpClient.domain.Features.Auth
{
    public class AuthService : IAuthService
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

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null)
            {
                responseModel = Result<LoginResponse>.NotFoundError();
                goto skip;
            }

            var checkPassowrd = new PasswordHasher().VerifyHashedPassword(user.Password, request.Password);

            if (checkPassowrd != PasswordVerificationResult.Success)
            {
                responseModel = Result<LoginResponse>.BadRequestError("Credential is invalid!");
                goto skip;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,request.Name),
                new Claim(ClaimTypes.Email,request.Email),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            responseModel = Result<LoginResponse>.Success(new LoginResponse { ClaimsPrincipal = principal });

        skip:

            return responseModel;
        }
        #endregion


        #region Register
        public async Task<Result<RegisterResponse>> Register(RegisterRequest request)
        {
            var responseModel = new Result<RegisterResponse>();

            var isExist = await _context.Users.AnyAsync(x => x.Email == request.Email);

            if (isExist)
            {
                responseModel = Result<RegisterResponse>.BadRequestError("User is Already Exist");
                goto skip;
            }

            var hashPassword = new PasswordHasher().HashPassword(request.Password);

            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashPassword,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Users.Add(newUser);
            var result = await _context.SaveChangesAsync();

            var data = new RegisterResponse
            {
                User = await _context.Users
                .Where(x => x.Email == request.Email)
                .Select(x => x.Change()).FirstOrDefaultAsync() ?? new(),
            };

            responseModel = result >= 1
                ? Result<RegisterResponse>.Success(data, "Register Success")
                : Result<RegisterResponse>.SystemError("Register Fail");

        skip:
            return responseModel;

        }
        #endregion

    }
}
