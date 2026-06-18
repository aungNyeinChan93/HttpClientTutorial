using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.JwtAuth.ReqResModels
{
    public class JwtLoginRequest
    {
        public string Email { get; set; }  = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
