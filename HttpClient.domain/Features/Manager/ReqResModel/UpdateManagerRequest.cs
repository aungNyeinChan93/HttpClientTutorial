using HttpClient.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Manager.ReqResModel
{
    public class UpdateManagerRequest
    {
        public string? Name { get; set; } = string.Empty;

        public string? Country { get; set; } = string.Empty;

        public int? Age { get; set; }

    }
}
