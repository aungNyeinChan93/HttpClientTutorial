using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.JwtAuth.ReqResModels
{
    public class JwtRefreshTokenRequest
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
