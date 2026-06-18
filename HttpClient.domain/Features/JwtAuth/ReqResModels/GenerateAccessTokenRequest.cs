using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.JwtAuth.ReqResModels
{
    public class GenerateAccessTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
