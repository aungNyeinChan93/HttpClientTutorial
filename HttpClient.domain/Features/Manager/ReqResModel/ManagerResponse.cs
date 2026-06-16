using HttpClient.domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Manager.ReqResModel
{
    public class ManagerResponse
    {
        public ManagerDto Manager { get; set; } = new();
    }
}
