using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.JwtAuth.ReqResModels
{
    public class GenerateAccessTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}
