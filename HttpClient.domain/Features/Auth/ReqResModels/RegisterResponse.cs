using HttpClient.domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Auth.ReqResModels
{
    public class RegisterResponse
    {
        public UserDto User { get; set; } = new();
    }
}
