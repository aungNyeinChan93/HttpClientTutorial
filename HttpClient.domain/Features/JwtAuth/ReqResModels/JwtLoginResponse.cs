using HttpClient.domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.JwtAuth.ReqResModels
{
    public class JwtLoginResponse
    {

        public UserDto User { get; set; } = new();

        public string Token { get; set; } = string.Empty;

    }
}
