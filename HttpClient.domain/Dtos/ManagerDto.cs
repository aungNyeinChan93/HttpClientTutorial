using HttpClient.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HttpClient.domain.Dtos
{
    public class ManagerDto
    {
        public int ManagerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public int Age { get; set; }

        public TeamDto? Team { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
