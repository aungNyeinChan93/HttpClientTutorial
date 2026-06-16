using HttpClient.domain.Dtos;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace HttpClient.domain.Features.Auth.ReqResModels
{
    public class LoginResponse
    {

        //public UserDto User { get; set; } = new();

        //public string Token { get; set; } = string.Empty;


        public ClaimsPrincipal ClaimsPrincipal { get; set; } = new();
    }
}
