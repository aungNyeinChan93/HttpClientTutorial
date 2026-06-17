using HttpClient.domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Manager.ReqResModel
{
    public class ManagersResponse
    {
        public List<ManagerDto> Manager { get; set; } = new();
    }
}
