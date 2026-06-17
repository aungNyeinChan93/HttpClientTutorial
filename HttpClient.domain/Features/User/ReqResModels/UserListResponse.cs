using HttpClient.domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.User.ReqResModels
{
    public class UserListResponse
    {
        public List<UserDto> Users { get; set; } = new();
    }
}
