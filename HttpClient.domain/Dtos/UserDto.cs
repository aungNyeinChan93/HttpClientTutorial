using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public int? RoleId { get; set; }

        //public string Password { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }
    }
}
