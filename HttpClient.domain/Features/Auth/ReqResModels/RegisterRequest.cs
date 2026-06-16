using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Auth.ReqResModels
{
    public class RegisterRequest
    {
        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string Password { get; set; } = null!;
    }
}
