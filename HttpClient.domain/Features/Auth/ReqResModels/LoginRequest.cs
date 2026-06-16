using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Auth.ReqResModels
{
    public class LoginRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
