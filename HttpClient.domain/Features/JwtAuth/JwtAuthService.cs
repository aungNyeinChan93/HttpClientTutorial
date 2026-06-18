using Httpclient.AuthDatabase.Models;
using HttpClient.domain.Dtos;
using HttpClient.domain.Features.JwtAuth.ReqResModels;
using HttpClient.domain.Mappers;
using HttpClient.shared.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HttpClient.domain.Features.JwtAuth
{
    public class JwtAuthService
    {
        private readonly AuthDatabase _context;

        private readonly IConfiguration _configuration;

        public JwtAuthService(AuthDatabase context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //login
        public async Task<Result<JwtLoginResponse>> LoginAsync(JwtLoginRequest request)
        {

            var responseModel = new Result<JwtLoginResponse>();
            var user = await _context.Users.AsNoTracking()
                .Where(u => u.Email == request.Email)
                .Select(x => x.Change())
                .FirstOrDefaultAsync();

            if (user is null)
            {
                responseModel = Result<JwtLoginResponse>.NotFoundError("Invalid User");
                goto skip;
            }

            var token = await this.GenerateTokenAsync(user);

            if (token is null || string.IsNullOrEmpty(token))
            {
                responseModel = Result<JwtLoginResponse>.SystemError("Generate Token Fail!");
                goto skip;
            }

            var refreshToken = this.GenerateRefreshToken();

            var saveRefreshToken = await this.SaveRefreshTokenAsync(new JwtRefreshTokenRequest
            {
                RefreshToken = refreshToken,
                UserId = user.Id,
            });

            if (!saveRefreshToken)
            {
                responseModel = Result<JwtLoginResponse>.InvalidDataError("Refresh token create fail!");
                goto skip;
            }

            var data = new JwtLoginResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                User = user,
            };

            responseModel = Result<JwtLoginResponse>.Success(data, "Login Success");

        skip:
            return responseModel;

        }

        //generate token
        public async Task<string> GenerateTokenAsync(UserDto user)
        {
            var role = await _context.Roles1.AsNoTracking().FirstOrDefaultAsync(r => r.Id == user.RoleId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Name),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Role ,role?.Name ?? "user" ),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(15),
                        signingCredentials: credential);



            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        // GenerateRefreshToken
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng =
                RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        //SaveRefreshToken
        public async Task<bool> SaveRefreshTokenAsync(JwtRefreshTokenRequest request)
        {
            var token = new RefreshToken
            {
                UserId = request.UserId,
                Token = request.RefreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _context.RefreshTokens.AddAsync(token);

            var result = await _context.SaveChangesAsync();
            return result >= 1 ? true : false;
        }


        //Generate Access Token From RefreshToken
        public async Task<Result<GenerateAccessTokenResponse>> GenerateAccessTokenByRefreshTokenAsync(GenerateAccessTokenRequest request)
        {
            var responseModel = new Result<GenerateAccessTokenResponse>();

            var storeToken = await _context.RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (storeToken is null)
            {
                responseModel = Result<GenerateAccessTokenResponse>.BadRequestError("refreshToken is invalid!");
                goto skip;
            }

            if (storeToken.ExpiresAt < DateTime.Now)
            {
                responseModel = Result<GenerateAccessTokenResponse>.InvalidDataError("Token Expire!");
                goto skip;
            }

            var user = await _context.Users
                .Where(x=>x.Id == storeToken.UserId)
                .Select(x=>x.Change()).FirstOrDefaultAsync();

            if (user is null)
            {
                responseModel = Result<GenerateAccessTokenResponse>.NotFoundError("Authorize User not found!");
                goto skip;
            }

            var token = await this.GenerateTokenAsync(user);

            if (token is null)
            {
                responseModel = Result<GenerateAccessTokenResponse>.InvalidDataError("Access Token Invalid!");
                goto skip;
            }

            var data = new GenerateAccessTokenResponse
            {
                AccessToken = token,
            };

            responseModel = Result<GenerateAccessTokenResponse>.Success(data,"Generate Access Token Success!");

        skip:
            return responseModel;
        }
    }
}
